using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoFacturaPeriodo : BaseController<look.domain.entities.factura.EstadoFacturaPeriodo>
    {
        public EstadoFacturaPeriodo(IEstadoFacturaPeriodoService service) : base(service)
        {
        }

        protected override int GetEntityId(look.domain.entities.factura.EstadoFacturaPeriodo entity)
        {
            return entity.Id;
        }
    }
}
