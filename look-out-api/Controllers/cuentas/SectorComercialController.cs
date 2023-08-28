using look.Application.interfaces;
using look.Application.interfaces.cuentas;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.cuentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorComercialController : BaseController<SectorComercial>
    {
        public SectorComercialController(ISectorComercialService service) : base(service)
        {
        }

        protected override int GetEntityId(SectorComercial entity)
        {
            return entity.SecId;
        }
    }
}
