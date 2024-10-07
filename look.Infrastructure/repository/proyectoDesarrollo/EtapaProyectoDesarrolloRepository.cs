using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyectoDesarrollo
{
    public class EtapaProyectoDesarrolloRepository : Repository<EtapaProyectoDesarrollo>, IEtapaProyectoDesarrolloRepository
    {
        public EtapaProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
