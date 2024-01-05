using look.domain.entities.admin;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class NovedadesRepository: Repository<Novedades>, INovedadesRepository
    {
        public NovedadesRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Novedades>> GetByProjectIdPersonId(ProyectoParticipante proyectoParticipante)
        {
            return await _dbContext.Novedades
                .Where(p => p.idProyecto == proyectoParticipante.PryId)
                .Where(p => p.idPersona == proyectoParticipante.PerId)
                .Where(p=>p.IdTipoNovedad==Novedades.ConstantesTipoNovedad.licencia)
                .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<List<Novedades>> GetComplete()
        {
            return await _dbContext.Novedades
                .Include(p=>p.Proyecto)
                .Include(p=>p.Persona)
                .Include(p=>p.Perfil)
                .Include(p=>p.TipoNovedades)
                .ToListAsync();
        }
    }
}

