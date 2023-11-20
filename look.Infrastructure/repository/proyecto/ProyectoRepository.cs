using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyecto
{
    public class ProyectoRepository : Repository<Proyecto>, IProyectoRepository
    {
        public ProyectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Proyecto>> GetComplete()
        {
            return await _dbContext.Proyecto
                .Include(p=>p.Cliente)
                .Include(p=>p.TipoServicio)
                .Include(p=>p.Pais)
                .ToListAsync();
        }
    }
}
