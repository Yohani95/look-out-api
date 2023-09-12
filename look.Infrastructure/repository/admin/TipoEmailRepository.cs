using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class TipoEmailRepository: Repository<TipoEmail>, ITipoEmailRepository
    {
        public TipoEmailRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TipoEmail>> GetComplete()
        {
            return await _dbContext.TipoEmail.ToListAsync();
        }
    }
}


