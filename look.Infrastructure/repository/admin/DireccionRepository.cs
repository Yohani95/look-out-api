using look.domain.entities.world;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class DireccionRepository: Repository<Direccion>, IDireccionRepository
    {
        public DireccionRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Direccion>> GetComplete()
        {
            return await _dbContext.Direccion.Include(e=>e.Cli).Include(e=>e.Per).Include(e=>e.Tdi).ToListAsync();
        }
        
        public async Task<List<Direccion>> ListCompleteByIdPersona(int id)
        {
            return await _dbContext.Direccion.Include(e=>e.Cli).Include(e=>e.Per).Include(e=>e.Tdi).Where(p => p.PerId == id).ToListAsync();
        }
    }
}

