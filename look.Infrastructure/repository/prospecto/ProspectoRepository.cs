using look.domain.entities.prospecto;
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

namespace look.Infrastructure.repository.prospecto
{
    public class ProspectoRepository : Repository<Prospecto>, IProspectoRepository
    {
        public ProspectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public async new Task<IEnumerable<Prospecto>> GetAllAsync()
        {
            return await _dbContext.Prospecto
                .Include(p => p.Cliente).ThenInclude(c => c.SectorComercial)
                .Include(p => p.Kam)
                .Include(p => p.Contacto)
                .Include(p => p.EstadoProspecto)
                .ToListAsync();
        }
    }
}
