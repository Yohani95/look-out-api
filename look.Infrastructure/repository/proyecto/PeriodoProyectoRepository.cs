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
        
        public async Task<List<ProyectoParticipante>> GetListProyectoParticipante(PeriodoProyecto proyectoDto)
        {
            var startDate = proyectoDto.FechaPeriodoDesde;
            var endDate = proyectoDto.FechaPeriodoHasta;
 
            var queryWithNullEndDate = _dbContext.ProyectoParticipante
                .Where(p => p.PryId == proyectoDto.PryId && p.FechaTermino == null &&
                            p.FechaAsignacion >= startDate && p.FechaAsignacion<= endDate);
 
            var queryWithValidEndDate = _dbContext.ProyectoParticipante
                .Where(p => p.PryId == proyectoDto.PryId && p.FechaTermino != null &&
                            p.FechaAsignacion <= startDate && p.FechaTermino >= endDate);
 
            var combinedQuery = queryWithNullEndDate.Union(queryWithValidEndDate);
 
            return await combinedQuery.Include(p => p.Proyecto).ToListAsync();
        }
    }
}

