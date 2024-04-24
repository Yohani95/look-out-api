using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.soporte
{
    public class HorasUtilizadasRepository : Repository<HorasUtilizadas>, IHorasUtilizadasRepository
    {
        public HorasUtilizadasRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<HorasUtilizadas>> getAllHorasByIdSoporte(int id)
        {
            return await _dbContext.HorasUtilizadas.Where(h=>h.IdSoporte == id).ToListAsync();
        }

        public new async Task<HorasUtilizadas> GetByIdAsync(int id)
        {
            return await _dbContext.HorasUtilizadas.Include(h=>h.Soporte).ThenInclude(s=>s.Cliente).FirstOrDefaultAsync(h=>h.Id==id);
        }
    }
}
