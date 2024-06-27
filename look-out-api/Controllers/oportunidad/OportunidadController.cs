using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class OportunidadController : BaseController<Oportunidad>
    {
        public OportunidadController(IOportunidadService service) : base(service)
        {
        }

        protected override int GetEntityId(Oportunidad entity)
        {
            return entity.Id;
        }
    }
}
