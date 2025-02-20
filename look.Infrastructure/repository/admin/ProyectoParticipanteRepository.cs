using look.domain.entities.admin;
using look.domain.entities.proyecto;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class ProyectoParticipanteRepository : Repository<ProyectoParticipante>, IProyectoParticipanteRepository
    {
        public ProyectoParticipanteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProyectoParticipante>> GetAllEntitiesByIdsProject(List<int> ids)
        {
            var data = await _dbContext.ProyectoParticipante
                .Include(p=>p.Persona)
                .Include(p=>p.Proyecto)
                .Where(p => ids.Contains((int)p.PryId))
                    .ToListAsync();
            return data;
        }

        public async Task<List<ProyectoParticipante>> GetComplete()
        {
            return await _dbContext.ProyectoParticipante.Include(e => e.Persona).Include(e => e.Proyecto).Include(e => e.Car).Include(e => e.Perfil).ToListAsync();
        }

        public async Task<List<ProyectoParticipante>> GetParticipanteByIdProAndDate(PeriodoProyecto periodo)
        {
            var startDate = periodo.FechaPeriodoDesde;
            var endDate = periodo.FechaPeriodoHasta;
            var filteredData = await _dbContext.ProyectoParticipante
            .Where(p => p.PryId == periodo.PryId &&
                ((p.FechaTermino == null && p.FechaAsignacion <= endDate) ||
                (p.FechaAsignacion <= endDate && p.FechaTermino >= startDate)))
                .ToListAsync();

            return filteredData;
        }
    }
}

