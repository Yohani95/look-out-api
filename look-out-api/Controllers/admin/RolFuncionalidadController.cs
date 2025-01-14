using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolFuncionalidadController : BaseController<RolFuncionalidad>
    {
        public RolFuncionalidadController(IRolFuncionalidadService service) : base(service)
        {
        }

        protected override int GetEntityId(RolFuncionalidad entity)
        {
            return entity.Id;
        }
    }
}
