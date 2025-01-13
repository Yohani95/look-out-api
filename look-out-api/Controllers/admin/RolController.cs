using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : BaseController<Rol>
    {
        public RolController(IRolService service) : base(service)
        {
        }

        protected override int GetEntityId(Rol entity)
        {
            return entity.RolId;
        }
    }
}
