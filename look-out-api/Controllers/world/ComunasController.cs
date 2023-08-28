using look.Application.interfaces;
using look.Application.interfaces.world;
using look.domain.entities.world;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.world
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunasController : BaseController<Comuna>
    {
        public ComunasController(IComunaService service) : base(service)
        {
        }

        protected override int GetEntityId(Comuna entity)
        {
            return entity.ComId;
        }
    }
}
