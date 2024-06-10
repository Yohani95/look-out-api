using look.domain.entities.oportunidad;
using look.domain.interfaces.oportunidad;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.oportunidad
{
    public class NovedadOportunidadRepository : Repository<NovedadOportunidad>, INovedadOportunidadRepository
    {
        public NovedadOportunidadRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<NovedadOportunidad>> GetByIdOportunidad(int id)
        {
            return await _dbContext.NovedadOportunidades.Where(n=>n.IdOportunidad==id).ToListAsync();
        }
    }
}
