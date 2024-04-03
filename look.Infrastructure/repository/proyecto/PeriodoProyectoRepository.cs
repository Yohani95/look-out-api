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
            DateTime fechaDesde = periodo.FechaPeriodoDesde.Value.Date;
            DateTime fechahasta = periodo.FechaPeriodoHasta.Value.Date;
            return await _dbContext.PeriodoProyectos
                .FirstOrDefaultAsync(p => p.FechaPeriodoHasta == fechahasta
                && p.FechaPeriodoDesde == fechaDesde && p.PryId==periodo.PryId);
        }

        public async Task<List<PeriodoProyecto>> GetComplete()
        {
            return await _dbContext.PeriodoProyectos
                .Include(p=>p.Proyecto)
                .ToListAsync();
        }

        public async Task<PeriodoProyecto> GetPeriodoProyectoById(int id)
        {
            return await _dbContext.PeriodoProyectos
                .Include(p=>p.Proyecto)
                .ThenInclude(p=>p.Cliente).FirstOrDefaultAsync(p=>p.id==id);
        }
    }
}

