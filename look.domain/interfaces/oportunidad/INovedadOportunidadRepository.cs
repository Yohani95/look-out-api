using look.domain.entities.oportunidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.oportunidad
{
    public interface INovedadOportunidadRepository:IRepository<NovedadOportunidad>
    {
        Task<List<NovedadOportunidad>> GetByIdOportunidad(int id);
    }
}
