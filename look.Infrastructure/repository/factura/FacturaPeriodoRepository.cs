using look.domain.entities.factura;
using look.domain.interfaces.factura;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.factura
{
    public class FacturaPeriodoRepository : Repository<FacturaPeriodo>, IFacturaPeriodoRepository
    {
        public FacturaPeriodoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
