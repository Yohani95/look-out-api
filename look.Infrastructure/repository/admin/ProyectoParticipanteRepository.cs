using look.domain.entities.admin;
using look.domain.entities.proyecto;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class ProyectoParticipanteRepository :Repository<ProyectoParticipante>, IProyectoParticipanteRepository
    {
        public ProyectoParticipanteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProyectoParticipante>> GetComplete()
        {
            return await _dbContext.ProyectoParticipante.Include(e=>e.Persona).Include(e=>e.Proyecto).Include(e=>e.Car).Include(e=>e.Perfil).ToListAsync();
        }

        public async Task<List<ProyectoParticipante>> GetParticipanteByIdProAndDate(PeriodoProyecto periodo)
        {
            var startDate = periodo.FechaPeriodoDesde;
            var endDate = periodo.FechaPeriodoHasta;

            var queryWithNullEndDate = _dbContext.ProyectoParticipante
                .Where(p => p.PryId == periodo.PryId && p.FechaTermino == null &&
                            p.FechaAsignacion >= startDate && p.FechaAsignacion <= endDate);

            var queryWithValidEndDate = _dbContext.ProyectoParticipante
                .Where(p => p.PryId == periodo.PryId && p.FechaTermino != null &&
                            p.FechaAsignacion <= startDate && p.FechaTermino >= endDate);

            var combinedQuery = queryWithNullEndDate.Union(queryWithValidEndDate);

            return await combinedQuery.Include(p => p.Proyecto).ToListAsync();
        }
    }
}

