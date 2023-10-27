using look.domain.entities.admin;
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
    }
}

