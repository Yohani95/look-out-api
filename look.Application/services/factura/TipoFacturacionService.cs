
using look.Application.interfaces.cuentas;
using look.Application.interfaces.factura;
using look.domain.entities.cuentas;
using look.domain.entities.factura;
using look.domain.interfaces;
using look.domain.interfaces.factura;

namespace look.Application.services.factura
{
    public class TipoFacturacionService : Service<TipoFacturacion>, ITipoFacturacionService
    {
        public TipoFacturacionService(ITipoFacturacionRepository repository) : base(repository)
        {
        }
    }
}
