using look.Application.interfaces.cuentas;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.interfaces;
using look.domain.interfaces.cuentas;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.cuentas
{
    public class ClienteService : Service<Cliente>, IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IClientePersonaService _clientePersonaService;
        private readonly IUnitOfWork _unitOfWork;
        public ClienteService(IClienteRepository repository, IClientePersonaService clientePersonaService,IUnitOfWork unitOfWork) : base(repository)
        {
            _repository = repository;
            _clientePersonaService = clientePersonaService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> CreateWithEntities(Cliente cliente, List<int> idPersons)
        {
            try
            {
                if (cliente == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El cliente proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();

                var user = await _repository.AddAsync(cliente);
                foreach (var personId in idPersons)
                {
                    var personaCliente = new ClientePersona
                    {
                        CarId = null,
                        CliId = user.CliId,
                        CliVigente = (sbyte?)user.EclId,
                        PerId = personId // Usar el ID de la persona actual
                    };
                    await _clientePersonaService.AddAsync(personaCliente);
                }

                await _unitOfWork.CommitAsync();

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El cliente Creado con exito"
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }
        public async Task<ServiceResult> EditWithEntities(int clientId, Cliente cliente, List<int> idPersons)
        {
            try
            {
                if (cliente == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El cliente proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();

                var existingCliente = await _repository.GetByIdAsync(clientId);
                if (existingCliente == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.NotFound, Message = "El cliente no existe." };
                }

                // Actualizar propiedades del cliente con los valores del cliente proporcionado
                await _repository.UpdateAsync(cliente);

                // Actualizar o eliminar las instancias existentes de ClientePersona según los idPersons proporcionados
                var existingPersonas = await _clientePersonaService.FindByClient(existingCliente.CliId);
                foreach (var persona in existingPersonas)
                {
                    if (!idPersons.Contains((int)persona.PerId))
                    {
                        await _clientePersonaService.DeleteAsync(persona);
                    }
                }

                foreach (var personId in idPersons)
                {
                    if (!existingPersonas.Any(p => p.PerId == personId))
                    {
                        var personaCliente = new ClientePersona
                        {
                            CarId = null,
                            CliId = existingCliente.CliId,
                            CliVigente = (sbyte?)existingCliente.EclId,
                            PerId = personId
                        };
                        await _clientePersonaService.AddAsync(personaCliente);
                    }
                }

                await _unitOfWork.CommitAsync();

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El cliente ha sido actualizado con éxito"
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }

        public async Task<ServiceResult> DeleteWithEntities(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El cliente proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();
                await _clientePersonaService.DeleteByClient(cliente.CliId);
                await _repository.DeleteAsync(cliente);

                await _unitOfWork.CommitAsync();

                return new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "El cliente eliminado con exito"
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InternalServerError, Message = $"Error interno del servidor: {ex.Message}" };
            }
        }

        public async Task<List<Cliente>> GetAllWithEntities()
        {
            return await _repository.GetAllWithEntities();
        }

        public async Task<ResponseGeneric<List<int>>> GetAllWithContactandKam(int clientId)
        {
            try
            {
                var client=await _repository.GetByIdAsync(clientId);
                if (client == null)
                {
                    var invalidInputResult = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.InvalidInput,
                        Message = "El cliente proporcionado es nulo."
                    };

                    return new ResponseGeneric<List<int>>
                    {
                        serviceResult = invalidInputResult,
                        Data = null // Puedes dejar la lista vacía o null según tu necesidad
                    };
                }

                await _unitOfWork.BeginTransactionAsync();
                var existingPersonas = await _clientePersonaService.FindByClient(client.CliId);
                await _unitOfWork.CommitAsync();
                var personaIds = existingPersonas.Select(persona => persona.PerId).ToList();

                var successResult = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "Solicitud Exitosa de clientes"
                };

                return new ResponseGeneric<List<int>>
                {
                    serviceResult = successResult,
                    Data = personaIds.Select(id => id.GetValueOrDefault()).ToList()
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = $"Error interno del servidor: {ex.Message}"
                };

                return new ResponseGeneric<List<int>>
                {
                    serviceResult = errorResult,
                    Data = null // Puedes dejar la lista vacía o null según tu necesidad
                };
            }
        }
    }
}
