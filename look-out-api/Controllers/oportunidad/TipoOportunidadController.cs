using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoOportunidadController : BaseController<TipoOportunidad>
    {
        public TipoOportunidadController(ITipoOportunidadService service) : base(service)
        {
        }

        protected override int GetEntityId(TipoOportunidad entity)
        {
            return entity.Id;
        }
    }
}
