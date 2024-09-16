using look.domain.entities.licencia;
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
    public class TarifarioVentaLicenciaRepository : Repository<TarifarioVentaLicencia>, ITarifarioVentaLicenciaRepository
    {
        public TarifarioVentaLicenciaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<TarifarioVentaLicencia>> GetAllAsync()
        {
            return await _dbContext.TarifarioVentaLicencias
                .Include(t => t.TipoLicencia)
                .Include(t => t.MarcaLicencia)
                .Include(t => t.MayoristaLicencia)
                .Include(t => t.Moneda)
                .ToListAsync();
        }

        public async Task<IEnumerable<TarifarioVentaLicencia>> GetAllEntitiesByIdLicense(int idLicencia)
        {
            return await _dbContext.TarifarioVentaLicencias
                .Include(t => t.TipoLicencia)
                .Include(t => t.MarcaLicencia)
                .Include(t => t.MayoristaLicencia)
                .Include(t => t.Moneda)
                .Where(t => t.IdVentaLicencia == idLicencia)
                .ToListAsync();
        }
    }
}
