using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrigenOportunidadController : BaseController<OrigenOportunidad>
    {
        public OrigenOportunidadController(IOrigenOportunidadService service) : base(service)
        {
        }

        protected override int GetEntityId(OrigenOportunidad entity)
        {
            return entity.Id;
        }
    }
}
