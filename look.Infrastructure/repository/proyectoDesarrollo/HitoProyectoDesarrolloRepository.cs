using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyectoDesarrollo
{
    public class HitoProyectoDesarrolloRepository : Repository<HitoProyectoDesarrollo>, IHitoProyectoDesarrolloRepository
    {
        public HitoProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<HitoProyectoDesarrollo>> GetAllAsync()
        {
            return await _dbContext.HitoProyectoDesarrollos
                          .Include(p => p.TipoHitoProyectoDesarrollo)
                          .ToListAsync();
        }

        public async Task<List<HitoProyectoDesarrollo>> GetAllByIdProyecto(int id)
        {
            return await _dbContext.HitoProyectoDesarrollos
                    .Include(p => p.TipoHitoProyectoDesarrollo)
                    .Where(p => p.IdProyectoDesarrollo == id)
                    .ToListAsync();
        }
    }
}
