using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaAdaptacionController : BaseController<FacturaAdaptacion>
    {
        public FacturaAdaptacionController(IFacturaAdaptacionService service) : base(service)
        {
        }

        protected override int GetEntityId(FacturaAdaptacion entity)
        {
            return entity.Id;
        }
    }
}
