using look.domain.dto.admin;
using look.domain.entities.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.cuentas
{
    public interface IClientePersonaRepository:IRepository<ClientePersona>
    {
        Task DeleteByClient(int id);
        Task<List<ClientePersona>> FindByClient(int id);
        Task<ClientePersona> FindByClientKam(int id);
        Task<List<ClientePersona>> GetAllClientRelations();
        Task<ClientePersona> GetClientePersonaDTOById(int id);
    }
}
