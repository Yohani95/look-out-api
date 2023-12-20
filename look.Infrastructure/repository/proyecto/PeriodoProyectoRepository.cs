using look.domain.entities.admin;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class PeriodoProyectoRepository: Repository<PeriodoProyecto>, IPeriodoProyectoRepository
    {
        public PeriodoProyectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PeriodoProyecto>> GetComplete()
        {
            return await _dbContext.PeriodoProyectos
                .Include(p=>p.Proyecto)
                .ToListAsync();
        }
    }
}

