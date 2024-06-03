using look.Application.interfaces;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoOportunidadController : BaseController<DocumentoOportunidad>
    {
        public DocumentoOportunidadController(IService<DocumentoOportunidad> service) : base(service)
        {
        }

        protected override int GetEntityId(DocumentoOportunidad entity)
        {
            return  entity.Id;
        }
    }
}
