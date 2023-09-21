using look.Application.interfaces.admin;
using look.Application.interfaces.cuentas;
using look.domain.dto.admin;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.interfaces.admin;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look.Application.services.admin
{
    public class PersonaService : Service<Persona>, IPersonaService

    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientePersonaService _clientePersonaService;
        public PersonaService(IPersonaRepository repository, IUnitOfWork unitOfWork,IClientePersonaService clientePersonaService) : base(repository)
        {
            _personaRepository = repository;
            _unitOfWork = unitOfWork;
            _clientePersonaService = clientePersonaService;
        }

        public async Task<ServiceResult> Create(PersonaDTO personaDTO)
        {
            _logger.Information("Crear Persona");
            try
            {
                if(personaDTO == null) {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "persona proporcionado es nulo." };
                }
                await _unitOfWork.BeginTransactionAsync();
                var persona=await _personaRepository.AddAsync(personaDTO.Persona);
                var personaCliente = new ClientePersona
                {
                    CarId = null,
                    CliId = personaDTO.IdCliente != null ? personaDTO.IdCliente : 0 ,
                    CliVigente = (sbyte?) 1,
                    PerId = persona.Id // Usar el ID de la persona actual
                };
                await _clientePersonaService.AddAsync(personaCliente);
                await _unitOfWork.CommitAsync();
                _logger.Information("El Contacto Creado con exito");

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El Contacto Creado con exito"
                };

            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error("Error interno del servidor: " + ex.ToString());
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
                
            }
        }

        public async Task<ServiceResult> Delete(int id)
        {
            _logger.Information("Eliminar Persona");
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var existingPersona = await _personaRepository.GetByIdAsync(id);

                if (existingPersona == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "La persona no fue encontrada." };
                }
                var clientesPersona = await _clientePersonaService.GetAllAsync(); // Obtiene todos los registros

                var clientePersonaFiltrado = clientesPersona
                    .Where(clientePersona =>
                          clientePersona.PerId == existingPersona.Id).FirstOrDefault();
                await _personaRepository.DeleteAsync(existingPersona);
                if (clientePersonaFiltrado != null )
                {
                    await _clientePersonaService.DeleteAsync(clientePersonaFiltrado);
                }

                await _unitOfWork.CommitAsync();
                _logger.Information("La Persona ha sido eliminada con éxito");

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "La Persona ha sido eliminada con éxito"
                };

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error("Error interno del servidor: " + ex.ToString());
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }

        /// <summary>
        /// actulizar persona y clientepersona
        /// </summary>
        /// <param name="personaDTO"> objeto </param>
        /// <returns>retorna un servicioresult </returns>
        public async Task<ServiceResult> Edit(int id, PersonaDTO personaDTO)
        {
            _logger.Information("Editar Persona");
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (personaDTO == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El objeto persona proporcionado es nulo." };
                }

                var existingPersona = await _personaRepository.GetByIdAsync(id);

                if (existingPersona == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "La persona no fue encontrada." };
                }
                var clientesPersona = await _clientePersonaService.GetAllAsync(); // Obtiene todos los registros

                var clientePersonaFiltrado = clientesPersona
                    .Where(clientePersona =>
                          clientePersona.PerId == personaDTO.Persona.Id).FirstOrDefault();

                if (clientePersonaFiltrado != null && clientePersonaFiltrado.CliId!=personaDTO.IdCliente)
                {
                    clientePersonaFiltrado.CliId = personaDTO.IdCliente; 
                    await _clientePersonaService.UpdateAsync(clientePersonaFiltrado);
                }

                existingPersona.PerNombres = personaDTO.Persona.PerNombres;
                existingPersona.PerApellidoPaterno = personaDTO.Persona.PerApellidoPaterno;
                existingPersona.PerApellidoMaterno = personaDTO.Persona.PerApellidoMaterno;
                existingPersona.PerIdNacional=personaDTO.Persona.PerIdNacional;
                existingPersona.PerFechaNacimiento = personaDTO.Persona.PerFechaNacimiento;
                await _personaRepository.UpdateAsync(existingPersona);
                await _unitOfWork.CommitAsync();
                _logger.Information("La Persona ha sido editada con éxito");

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "La Persona ha sido editada con éxito"
                };

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error("Error interno del servidor: " + ex.ToString());
                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }

        public async Task<List<Persona>> GetAllByType(int typePersonId)
        {
            try
            {
                return await _personaRepository.GetAllByType(typePersonId);
            }catch(Exception ex)
            {
                _logger.Error("Error interno del servidor: " + ex.ToString());
                return null;
            }
        }

        public async Task<ResponseGeneric<List<PersonaDTO>>> GetAllContactEnteties()
        {
            _logger.Information("Listando contactos con sus entidades");
            try
            {
                var contact = await _personaRepository.GetAllContactEnteties();

                if (contact == null)
                {
                    var invalidInputResult = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.InvalidInput,
                        Message = "Sin Datos."
                    };

                    return new ResponseGeneric<List<PersonaDTO>>
                    {
                        serviceResult = invalidInputResult,
                        Data = null 
                    };
                }
                var result = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "Listaado de Contactos."
                };
                return new ResponseGeneric<List<PersonaDTO>>
                {
                    serviceResult = result,
                    Data = contact 
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error("Error interno del servidor: " + ex.ToString());
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = $"Error interno del servidor: {ex.Message}"
                };

                return new ResponseGeneric<List<PersonaDTO>>
                {
                    serviceResult = errorResult,
                    Data = null // Puedes dejar la lista vacía o null según tu necesidad
                };
            }
        }
    }
}
