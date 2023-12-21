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

        public async Task<PeriodoProyecto> GetByPeriodoRange(PeriodoProyecto periodo)
        {
            return await _dbContext.PeriodoProyectos
                .FirstAsync(p => p.FechaPeriodoHasta == periodo.FechaPeriodoHasta
                && p.FechaPeriodoDesde == periodo.FechaPeriodoDesde);
        }

        public async Task<List<PeriodoProyecto>> GetComplete()
        {
            return await _dbContext.PeriodoProyectos
                .Include(p=>p.Proyecto)
                .ToListAsync();
        }
    }
}

