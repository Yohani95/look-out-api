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

        public async Task<ServiceResult> CreateWithEntities(Cliente cliente, int idPerson)
        {
            try
            {
                if (cliente == null)
                {
                    return new ServiceResult { IsSuccess = false, MessageCode = ServiceResultMessage.InvalidInput, Message = "El cliente proporcionado es nulo." };
                }

                await _unitOfWork.BeginTransactionAsync();

                await _repository.AddAsync(cliente);
                var personaCliente = new ClientePersona
                {
                    CarId = null,
                    CliId = cliente.CliId,
                    CliVigente = (sbyte?)cliente.EclId,
                    PerId = idPerson
                };
                await _clientePersonaService.AddAsync(personaCliente);

                await _unitOfWork.CommitAsync();

                return new ServiceResult { IsSuccess = true, MessageCode = ServiceResultMessage.Success,
                    Message = "El cliente Creado con exito"
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
    }
}
