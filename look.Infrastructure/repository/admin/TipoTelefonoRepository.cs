using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class TipoTelefonoRepository: Repository<TipoTelefono>, ITipoTelefonoRepository
    {
        public TipoTelefonoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TipoTelefono>> GetComplete()
        {
            return await _dbContext.TipoTelefono.ToListAsync();
        }
    }
}


