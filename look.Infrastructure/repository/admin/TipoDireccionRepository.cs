using look.domain.entities.world;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class TipoDireccionRepository: Repository<TipoDireccion>, ITipoDireccionRepository
    {
        public TipoDireccionRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TipoDireccion>> GetComplete()
        {
            return await _dbContext.TipoDireccion.ToListAsync();
        }
    }
}

