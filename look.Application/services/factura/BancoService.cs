using look.Application.interfaces.factura;
using look.domain.entities.factura;
using look.domain.interfaces;
using look.domain.interfaces.factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.factura
{
    internal class BancoService : Service<Banco>, IBancoService
    {
        public BancoService(IBancoRepository repository) : base(repository)
        {
        }
    }
}
