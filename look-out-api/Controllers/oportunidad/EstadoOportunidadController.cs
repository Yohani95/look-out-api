using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.cuentas;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoOportunidadController : BaseController<EstadoOportunidad>
    {
        public EstadoOportunidadController(IEstadoOportunidadService service) : base(service)
        {
        }

        protected override int GetEntityId(EstadoOportunidad entity)
        {
            return entity.Id;
        }
    }
}
