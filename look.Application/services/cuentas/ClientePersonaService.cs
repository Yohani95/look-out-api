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
    }
}
