using look.domain.entities.cuentas;
using look.domain.interfaces.cuentas;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.Cuentas
{
    public class SectorComercialRepository : Repository<SectorComercial>, ISectorComercialRepository
    {
        public SectorComercialRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
