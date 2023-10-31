using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class TipoNovedadesRepositoryRepository: Repository<TipoNovedades>, ITipoNovedadesRepository
    {
        public TipoNovedadesRepositoryRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TipoNovedades>> GetComplete()
        {
            return await _dbContext.TipoNovedades.ToListAsync();
        }
    }
}

