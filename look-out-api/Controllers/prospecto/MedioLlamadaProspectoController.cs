using look.Application.interfaces;
using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.prospecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedioLlamadaProspectoController : BaseController<MedioLLamadaProspecto>
    {
        public MedioLlamadaProspectoController(IMedioLlamadaProspectoService service) : base(service)
        {
        }

        protected override int GetEntityId(MedioLLamadaProspecto entity)
        {
            return entity.Id;
        }
    }
}
