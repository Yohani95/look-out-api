using look.domain.entities.licencia;
using look.domain.entities.oportunidad;
using look.domain.interfaces.licencia;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.licencia
{
    public class VentaLicenciaRepository : Repository<VentaLicencia>, IVentaLicenciaRepository
    {
        public VentaLicenciaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<IEnumerable<VentaLicencia>> GetAllAsync()
        {
            return await _dbContext.VentaLicencias
                .Include(o => o.Cliente)
                .Include(o => o.EstadoVentaLicencia)
                .Include(o => o.EmpresaPrestadora)
                .Include(o => o.Moneda)
                .Include(o => o.Pais)
                .Include(o => o.Kam)
                .ToListAsync();
        }
        public new async Task<VentaLicencia> GetByIdAsync(int id)
        {
            return await _dbContext.VentaLicencias
                .Include(o => o.Cliente)
                .Include(o => o.EstadoVentaLicencia)
                .Include(o => o.EmpresaPrestadora)
                .Include(o => o.Moneda)
                .Include(o => o.Pais)
                .Include(o => o.Kam)
                .FirstAsync(v=>v.Id==id);
        }
    }
}
