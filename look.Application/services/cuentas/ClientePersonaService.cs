using look.Application.interfaces.cuentas;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.interfaces;
using look.domain.interfaces.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.cuentas
{
    public class ClientePersonaService : Service<ClientePersona>, IClientePersonaService
    {
        private readonly IClientePersonaRepository _repository;
        public ClientePersonaService(IClientePersonaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task DeleteByClient(int id)
        {
            try
            {
                 await _repository.DeleteByClient(id);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<ClientePersona>> FindByClient(int id)
        {
            try
            {
                return await _repository.FindByClient(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ClientePersona> FindByClientKam(int id)
        {
            try
            {
                return await _repository.FindByClientKam(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseGeneric<List<ClientePersona>>> GetAllClientRelations()
        {
            try
            {
                var client = await _repository.GetAllClientRelations();
                if (client == null)
                {
                    var invalidInputResult = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.NotFound,
                        Message = "Sin clientes, nulo."
                    };

                    return new ResponseGeneric<List<ClientePersona>>
                    {
                        serviceResult = invalidInputResult,
                        Data = null // Puedes dejar la lista vacía o null según tu necesidad,
                    };
                }

              
                var successResult = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = "Solicitud Exitosa de clientes"
                };
                return new ResponseGeneric<List<ClientePersona>>
                {
                    serviceResult = successResult,
                    Data = client
                };
            }
            catch (Exception ex)
            {

                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = $"Error interno del servidor: {ex.Message}"
                };

                return new ResponseGeneric<List<ClientePersona>>
                {
                    serviceResult = errorResult,
                    Data = null // Puedes dejar la lista vacía o null según tu necesidad
                };
            }
        }
    }
}
