using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class ProyectoParticipanteRepositoryRepository :Repository<ProyectoParticipante>, IProyectoParticipanteRepository
    {
        public ProyectoParticipanteRepositoryRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProyectoParticipante>> GetComplete()
        {
            return await _dbContext.ProyectoParticipante.Include(e=>e.Per).Include(e=>e.Per).Include(e=>e.Pro).Include(e=>e.Car).ToListAsync();
        }
    }
}

