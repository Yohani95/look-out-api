using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.proyecto
{
    public interface IProyectoRepository : IRepository<Proyecto>
    {
        Task<List<Proyecto>> GetComplete();

        Task<List<Proyecto>> GetAllByClientId(int clientId);
    }
}
