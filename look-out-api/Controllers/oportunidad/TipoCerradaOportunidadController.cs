using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCerradaOportunidadController : BaseController<TipoCerradaOportunidad>
    {
        public TipoCerradaOportunidadController(ITipoCerradaOportunidadService service) : base(service)
        {
        }

        protected override int GetEntityId(TipoCerradaOportunidad entity)
        {
            return entity.Id;
        }
    }
}
