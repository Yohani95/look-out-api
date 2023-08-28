using look.Application.interfaces;
using look.Application.interfaces.cuentas;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.cuentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class GirosController : BaseController<Giro>
    {
        public GirosController(IGiroService service) : base(service)
        {
        }

        protected override int GetEntityId(Giro entity)
        {
            return entity.GirId;
        }
    }
}
