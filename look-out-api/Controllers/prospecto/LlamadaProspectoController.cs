using look.Application.interfaces;
using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.prospecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class LlamadaProspectoController : BaseController<LlamadaProspecto>
    {
        public LlamadaProspectoController(ILlamadaProspectoService service) : base(service)
        {
        }

        protected override int GetEntityId(LlamadaProspecto entity)
        {
            return entity.Id;
        }
    }
}
