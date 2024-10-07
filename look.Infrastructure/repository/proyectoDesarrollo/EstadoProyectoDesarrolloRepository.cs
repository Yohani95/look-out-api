
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
    internal class EstadoProyectoDesarrolloRepository : Repository<EstadoProyectoDesarrollo>, IEstadoProyectoDesarrolloRepository
    {
        public EstadoProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
