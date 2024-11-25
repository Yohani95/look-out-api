using look.Application.interfaces;
using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.prospecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoReunionProspectoController : BaseController<EstadoReunionProspecto>
    {
        public EstadoReunionProspectoController(IEstadoReunionProspectoService service) : base(service)
        {
        }

        protected override int GetEntityId(EstadoReunionProspecto entity)
        {
            return entity.Id;
        }
    }
}
