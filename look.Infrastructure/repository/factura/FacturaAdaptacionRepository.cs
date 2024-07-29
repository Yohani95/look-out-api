using look.domain.entities.factura;
using look.domain.interfaces.factura;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.factura
{
    public class FacturaAdaptacionRepository : Repository<FacturaAdaptacion>, IFacturaAdaptacionRepository
    {
        public FacturaAdaptacionRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<FacturaAdaptacion> GetAllByIdHoras(int id)
        {
            return await _dbContext.FacturaAdaptaciones.FirstAsync(x => x.IdHorasUtilizadas == id);
        }

        public async Task<FacturaAdaptacion> GetAllByIdSoporte(int id)
        {
            return await _dbContext.FacturaAdaptaciones.FirstAsync(x => x.IdSoporte == id);
        }

        public async Task<FacturaAdaptacion> GetAllEntitiesByIdLicense(int id)
        {
            return await _dbContext.FacturaAdaptaciones.FirstAsync(x => x.IdLicencia == id);
        }

        public async Task<FacturaAdaptacion> GetAllEntitiesByIdPeriod(int id)
        {
            return await _dbContext.FacturaAdaptaciones.FirstAsync(x => x.IdPeriodoProyecto == id);
        }
    }
}
