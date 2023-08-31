using look.domain.entities.Common;
using look.domain.entities.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.cuentas
{
    public interface IClientePersonaService:IService<ClientePersona>
    {
        Task<List<ClientePersona>> FindByClient(int id);
        Task DeleteByClient(int id);
        Task<ClientePersona> FindByClientKam(int id);
        Task<ResponseGeneric<List<ClientePersona>>> GetAllClientRelations();
    }
}
