using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicitacionOportunidadController : BaseController<LicitacionOportunidad>
    {
        public LicitacionOportunidadController(ILicitacionOportunidadService service) : base(service)
        {
        }

        protected override int GetEntityId(LicitacionOportunidad entity)
        {
            return entity.Id;
        }
    }
}
