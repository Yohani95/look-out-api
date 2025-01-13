using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionalidadController : BaseController<Funcionalidad>
    {
        public FuncionalidadController(IFuncionalidadService service) : base(service)
        {
        }

        protected override int GetEntityId(Funcionalidad entity)
        {
            return entity.Id;
        }
    }
}
