using look.domain.entities.prospecto;
using look.domain.interfaces.prospecto;
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
                .Include(p => p.Contacto).ThenInclude(c => c.TipoContactoProspecto)
                .Include(p => p.Contacto).ThenInclude(c => c.Pais)
                .Include(p => p.EstadoProspecto)
                .ToListAsync();
        }
        public async new Task<Prospecto> GetByIdAsync(int id)
        {
            return await _dbContext.Prospecto
                .Include(p => p.Cliente).ThenInclude(c => c.SectorComercial)
                .Include(p => p.Kam)
                .Include(p => p.Contacto).ThenInclude(c => c.TipoContactoProspecto)
                .Include(p => p.Contacto).ThenInclude(c => c.Pais)
                .Include(p => p.EstadoProspecto)
                .FirstAsync(p => p.Id == id);
        }
    }
}
