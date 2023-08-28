using look.Application.interfaces.cuentas;
using look.domain.entities.cuentas;
using look.domain.interfaces;
using look.domain.interfaces.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.cuentas
{
    public class SectorComercialService : Service<SectorComercial>, ISectorComercialService
    {
        public SectorComercialService(ISectorComercialRepository repository) : base(repository)
        {
        }
    }
}
