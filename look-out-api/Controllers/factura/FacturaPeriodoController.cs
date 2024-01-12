using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaPeriodoController : BaseController<FacturaPeriodo>
    {
        public FacturaPeriodoController(IFacturaPeriodoService service) : base(service)
        {
        }

        protected override int GetEntityId(FacturaPeriodo entity)
        {
            throw new NotImplementedException();
        }
    }
}
