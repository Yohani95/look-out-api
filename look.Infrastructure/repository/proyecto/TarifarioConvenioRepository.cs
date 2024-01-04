using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class TarifarioConvenioRepository:Repository<TarifarioConvenio>, ITarifarioConvenioRepository
    {
        public TarifarioConvenioRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TarifarioConvenio>> GetComplete()
        {
            return await _dbContext.TarifarioConvenio
                .Include(t=>t.Perfil)
                .Include(t=>t.Proyecto)
                .Include(t=>t.Moneda)
                .ToListAsync();
        }
        public async Task<TarifarioConvenio> GetbyIdEntities(int id)
        {
            return await _dbContext.TarifarioConvenio
                .Include(t => t.Perfil)
                .Include(t => t.Proyecto)
                .Include(t => t.Moneda)
                .FirstAsync(t=>t.TcId==id);
        }
    }
}

