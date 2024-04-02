using look.domain.entities.soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.soporte
{
    public interface ISoporteRepository:IRepository<Soporte>
    {
        Task<IEnumerable<Soporte>> GetAllEntities();
        Task<Soporte> GetAllEntitiesById(int id);
    }
}
