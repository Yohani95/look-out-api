using look.Application.interfaces;
using look.Application.interfaces.soporte;
using look.domain.entities.soporte;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.soporte
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DocumentosSoporteController : BaseController<DocumentosSoporte>
    {
        public DocumentosSoporteController(IDocumentosSoporteService service) : base(service)
        {
        }

        protected override int GetEntityId(DocumentosSoporte entity)
        {
            return entity.Id;
        }
    }
}
