using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data;
using look.Infrastructure.data.proyectoDesarrollo;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyectoDesarrollo
{
    public class EtapaPlanificacionProyectoDesarrolloRepository : Repository<EtapaPlanificacionProyectoDesarrollo>, IEtapaPlanificacionProyectoDesarrolloRepository
    {
        public EtapaPlanificacionProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
