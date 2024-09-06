using look.Application.interfaces;
using look.Application.interfaces.factura;
using look.domain.entities.factura;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.factura
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : BaseController<Banco>
    {
        public BancoController(IBancoService service) : base(service)
        {
        }

        protected override int GetEntityId(Banco entity)
        {
            return entity.Id;
        }
    }
}
