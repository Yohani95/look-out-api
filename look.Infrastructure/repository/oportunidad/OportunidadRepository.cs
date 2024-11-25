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
    public class OportunidadRepository : Repository<Oportunidad>, IOportunidadRepository
    {
        public OportunidadRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<Oportunidad>> GetAllAsync()
        {
            return await _dbContext.oportunidades
                .Include(o => o.Cliente)
                .Include(o => o.TipoOportunidad)
                .Include(o => o.EstadoOportunidad)
                .Include(o => o.EmpresaPrestadora)
                .Include(o => o.Moneda)
                .Include(o => o.Pais)
                .Include(o => o.PersonaKam)
                .ToListAsync();
        }
        public new async Task<Oportunidad> GetByIdAsync(int id)
        {
            return await _dbContext.oportunidades
                .Include(o => o.Cliente)
                .Include(o => o.TipoOportunidad)
                .Include(o => o.EstadoOportunidad)
                .Include(o => o.EmpresaPrestadora)
                .Include(o => o.Moneda)
                .Include(o => o.Pais)
                .Include(o => o.PersonaKam)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

    }
}
