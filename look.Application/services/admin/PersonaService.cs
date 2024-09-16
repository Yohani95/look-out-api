using look.Application.interfaces.admin;
using look.Application.interfaces.cuentas;
using look.domain.dto.admin;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.world;
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
        private readonly IEmailService _emailService;
        private readonly ITelefonoService _telefonoService;
        private readonly IDireccionService _direccionService;
        public PersonaService(IPersonaRepository repository, IUnitOfWork unitOfWork, IClientePersonaService clientePersonaService, IEmailService emailService, ITelefonoService telefonoService, IDireccionService direccionService) : base(repository)
        {
            _personaRepository = repository;
            _unitOfWork = unitOfWork;
            _clientePersonaService = clientePersonaService;
            _emailService = emailService;
            _telefonoService = telefonoService;
            _direccionService = direccionService;
        }

        public async Task<ServiceResult> Create(PersonaDTO personaDTO)
        {
            _logger.Information("Crear Persona");
            try
            {
                if (personaDTO == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "persona proporcionado es nulo." };
                }
                await _unitOfWork.BeginTransactionAsync();
                var persona = await _personaRepository.AddAsync(personaDTO.Persona);
                var personaCliente = new ClientePersona
                {
                    CarId = null,
                    CliId = personaDTO.IdCliente != null && personaDTO.IdCliente != 0 ? personaDTO.IdCliente : null,
                    CliVigente = (sbyte?)1,
                    PerId = persona.Id // Usar el ID de la persona actual
                };
                if (personaDTO.Emails.Count > 0)
                {
                    //agrega las email enviadas
                    foreach (var emailList in personaDTO.Emails)
                    {
                        var email = new Email();
                        email.CliId = personaDTO.IdCliente;
                        email.PerId = persona.Id;
                        email.EmaVigente = emailList.EmaVigente;
                        email.EmaPrincipal = emailList.EmaPrincipal;
                        email.TemId = emailList.TemId;
                        email.EmaEmail = emailList.EmaEmail;

                        await _emailService.AddAsync(email);
                    }
                }
                if (personaDTO.Telefonos.Count > 0)
                {
                    //agrega las telefonos enviadas
                    foreach (var telefonosList in personaDTO.Telefonos)
                    {
                        var telefono = new Telefono();
                        telefono.cliId = personaDTO.IdCliente;
                        telefono.perId = persona.Id;
                        telefono.telNumero = telefonosList.telNumero;
                        telefono.tteId = telefonosList.tteId;
                        telefono.telVigente = telefonosList.telVigente;
                        telefono.TelPrincipal = telefonosList.TelPrincipal;

                        await _telefonoService.AddAsync(telefono);
                    }
                }
                if (personaDTO.direcciones.Count > 0)
                {
                    //agrega las direcciones enviadas
                    foreach (var direccionList in personaDTO.direcciones)
                    {
                        var direccion = new Direccion();
                        direccion.PerId = persona.Id;
                        direccion.CliId = personaDTO.IdCliente;
                        direccion.DirCalle = direccionList.DirCalle;
                        direccion.DirNumero = direccionList.DirNumero;
                        direccion.ComId = direccionList.ComId;
                        direccion.DirBlock = direccionList.DirBlock;
                        direccion.TdiId = direccionList.TdiId;
                        direccion.DirPrincipal = direccionList.DirPrincipal;

                        await _direccionService.AddAsync(direccion);
                    }
                }

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
            catch (Exception ex)
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

                //elimina las email enviadas
                var emailListSearch = await _emailService.ListComplete();
                var emailListSearchFiltrado = emailListSearch.Where(d => d.PerId == id).ToList();
                if (emailListSearchFiltrado.Count > 0)
                {

                    foreach (var emailList in emailListSearchFiltrado)
                    {
                        await _emailService.DeleteAsync(emailList);
                    }
                }

                //elimina las telefonos enviadas
                var telefonoListSearch = await _telefonoService.ListComplete();
                var telefonoListSearchFiltrado = telefonoListSearch.Where(d => d.perId == id).ToList();
                if (telefonoListSearchFiltrado.Count > 0)
                {

                    foreach (var telefonosList in telefonoListSearchFiltrado)
                    {
                        await _telefonoService.DeleteAsync(telefonosList);
                    }
                }

                //elimina las direcciones enviadas
                var direccionListSearch = await _direccionService.ListComplete();
                var direccionListSearchFiltrado = direccionListSearch.Where(d => d.PerId == id).ToList();
                if (direccionListSearchFiltrado.Count > 0)
                {

                    foreach (var direccionList in direccionListSearchFiltrado)
                    {
                        await _direccionService.DeleteAsync(direccionList);
                    }
                }

                var clientePersonaFiltrado = clientesPersona
                    .Where(clientePersona =>
                          clientePersona.PerId == existingPersona.Id).FirstOrDefault();
                await _personaRepository.DeleteAsync(existingPersona);
                if (clientePersonaFiltrado != null)
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
                var clientePersona = await _clientePersonaService.GetByIdAsync((int)personaDTO.IdCliente);
                if (personaDTO.ClientePersona != null && personaDTO.IdCliente != personaDTO.ClientePersona.CliId)
                {

                    if (clientePersona != null)
                    {
                        await _clientePersonaService.DeleteAsync(personaDTO.ClientePersona);
                    }
                    else
                    {
                        personaDTO.ClientePersona.CliId = personaDTO.IdCliente;
                        await _clientePersonaService.UpdateAsync(clientePersona);
                    }
                }
                else
                {
                    personaDTO.ClientePersona.CliId = personaDTO.IdCliente;
                    await _clientePersonaService.UpdateAsync(clientePersona);
                }

                if (personaDTO.Emails != null && personaDTO.Emails.Count > 0)
                {
                    // Buscar y eliminar los correos electrónicos antiguos
                    var emailListSearch = await _emailService.ListComplete();
                    var emailListSearchFiltrado = emailListSearch.Where(d => d.PerId == id).ToList();
                    var emailListIds = personaDTO.Emails.Select(email => email.EmaId).ToList(); //comparar el id 

                    foreach (var emailList in emailListSearchFiltrado)
                    {
                        if (!emailListIds.Contains(emailList.EmaId))
                        {
                            await _emailService.DeleteAsync(emailList);
                        }
                    }

                    // Contador para rastrear si ya se encontró un email principal
                    var principalCount = 0;

                    // Agregar los nuevos correos electrónicos enviados
                    foreach (var emailList in personaDTO.Emails.Where(e => e.EmaId < 1)) // filtra donde sea menor 1, ya que lógicamente en la bbdd son autoincrement
                    {
                        emailList.PerId = id;

                        // Si es el email principal, establecerlo como principal y los demás como no principales
                        if (emailList.EmaPrincipal == 1)
                        {
                            principalCount++;
                            await _emailService.AddAsync(emailList);
                        }
                        else
                        {
                            // Si ya hay un principal, establecer los demás como no principales
                            if (principalCount > 0)
                            {
                                emailList.EmaPrincipal = 0;
                            }

                            await _emailService.AddAsync(emailList);
                        }
                    }

                    // Actualizar los emails existentes si ya hay uno principal
                    if (principalCount > 0)
                    {
                        var existingPrincipalEmail = emailListSearchFiltrado.FirstOrDefault(e => e.EmaPrincipal == 1);
                        if (existingPrincipalEmail != null)
                        {
                            existingPrincipalEmail.EmaPrincipal = 0;
                            await _emailService.UpdateAsync(existingPrincipalEmail);
                        }
                    }
                }

                if (personaDTO.Telefonos != null && personaDTO.Telefonos.Any())
                {
                    // Eliminar teléfonos antiguos
                    var oldPhones = await _telefonoService.ListComplete();

                    // Filtrar teléfonos existentes por el ID de persona
                    oldPhones = oldPhones.Where(p => p.perId == id).ToList();

                    var phoneIds = personaDTO.Telefonos.Select(phone => phone.telId);

                    foreach (var oldPhone in oldPhones.Where(p => !phoneIds.Contains(p.telId)))
                    {
                        await _telefonoService.DeleteAsync(oldPhone);
                    }

                    // Agregar nuevos teléfonos
                    foreach (var newPhone in personaDTO.Telefonos.Where(p => p.telId < 1))
                    {
                        // Nuevo teléfono, asignar IDs y agregar
                        newPhone.cliId = personaDTO.IdCliente;
                        newPhone.perId = id;
                        await _telefonoService.AddAsync(newPhone);
                    }
                }

                if (personaDTO.direcciones != null && personaDTO.direcciones.Any())
                {
                    // Obtener direcciones existentes del servicio
                    var oldAddresses = await _direccionService.ListComplete();

                    // Filtrar direcciones existentes por el ID de persona
                    oldAddresses = oldAddresses.Where(d => d.PerId == id).ToList();

                    // Eliminar direcciones antiguas que no están en la lista de nuevas direcciones
                    var addressIds = personaDTO.direcciones.Select(address => address.DirId);

                    foreach (var oldAddress in oldAddresses.Where(d => !addressIds.Contains(d.DirId)))
                    {
                        await _direccionService.DeleteAsync(oldAddress);
                    }

                    // Agregar o actualizar nuevas direcciones
                    foreach (var newAddress in personaDTO.direcciones.Where(d => d.DirId < 1))
                    {
                        // Nueva dirección, asignar IDs y agregar
                        newAddress.PerId = id;
                        newAddress.CliId = personaDTO.IdCliente;
                        await _direccionService.AddAsync(newAddress);
                        //else
                        //{
                        //    // Actualizar dirección existente
                        //    await _direccionService.UpdateAsync(newAddress);
                        //}
                    }
                }

                var existingPersona = await _personaRepository.GetByIdAsync(id);
                existingPersona.PerIdNacional = personaDTO.Persona.PerIdNacional;
                existingPersona.PerNombres = personaDTO.Persona.PerNombres;
                existingPersona.PerApellidoPaterno = personaDTO.Persona.PerApellidoPaterno;
                existingPersona.PerApellidoMaterno = personaDTO.Persona.PerApellidoMaterno;
                existingPersona.PaiId = personaDTO.Persona.PaiId;
                existingPersona.TpeId = personaDTO.Persona.TpeId;
                existingPersona.Cargo = personaDTO.Persona.Cargo;

                // Actualizar la entidad en el servicio de Persona
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
            }
            catch (Exception ex)
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
                    Message = "Listado de Contactos."
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

        public async Task<ResponseGeneric<List<PersonaDTO>>> GetAllContact()
        {
            _logger.Information("Listando contactos con sus entidades");
            try
            {
                var contact = await _personaRepository.GetAllContact();

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
                    Message = "Listado de Contactos."
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

        /// <summary>
        /// lista todas las persona de tipo contacto segun el id del cliente
        /// </summary>
        /// <param name="id">id de cliente esperado</param>
        /// <returns>retorna una lista de persona tipo contacto</returns>
        public async Task<ResponseGeneric<List<Persona>>> GetAllContactByIdClient(int id)
        {
            _logger.Information("Listando contactos segun id del cliente");
            try
            {
                var clientPerson = await _clientePersonaService.FindByClient(id);
                var contact = clientPerson.Select(cp => cp.Persona).ToList();

                if (contact == null || clientPerson == null)
                {
                    var invalidInputResult = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.InvalidInput,
                        Message = "Sin Datos."
                    };

                    return new ResponseGeneric<List<Persona>>
                    {
                        serviceResult = invalidInputResult,
                        Data = null
                    };
                }
                var result = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "Listado de Contactos segun el id del cliente."
                };
                return new ResponseGeneric<List<Persona>>
                {
                    serviceResult = result,
                    Data = contact.Where(p => p.TpeId == 3).ToList(),
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

                return new ResponseGeneric<List<Persona>>
                {
                    serviceResult = errorResult,
                    Data = null // Puedes dejar la lista vacía o null según tu necesidad
                };
            }
        }
    }
}
