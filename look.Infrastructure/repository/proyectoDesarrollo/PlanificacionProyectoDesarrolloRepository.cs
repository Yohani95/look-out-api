using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyectoDesarrollo
{
    internal class PlanificacionProyectoDesarrolloRepository : Repository<PlanificacionProyectoDesarrollo>, IPlanificacionProyectoDesarrolloRepository
    {
        public PlanificacionProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<PlanificacionProyectoDesarrollo>> GetAllAsync()
        {
            return await _dbContext.PlanificacionProyectoDesarrollos
                            .Include(p => p.Etapa)
                            .ToListAsync();
        }

        public async Task<List<PlanificacionProyectoDesarrollo>> GetAllByIdProyecto(int id)
        {
            return await _dbContext.PlanificacionProyectoDesarrollos
                .Include(p => p.Etapa)
                 .Where(n => n.IdProyectoDesarrollo == id)
                 .ToListAsync();
        }
    }
}
