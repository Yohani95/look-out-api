using look.Application.interfaces;
using look.Application.interfaces.licencia;
using look.domain.entities.licencia;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.licencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoLicenciaController : BaseController<DocumentoLicencia>
    {
        private readonly IDocumentoLicenciaService _documentoLicenciaService;
        public DocumentoLicenciaController(IDocumentoLicenciaService service) : base(service)
        {
            _documentoLicenciaService = service;
        }
        [HttpGet("getbyIdLicencia/{id}")]
        public async Task<IActionResult> getbyIdLicencia(int id)
        {
            var result = await _documentoLicenciaService.GetByIdVentaLicencia(id);
            return Ok(result);
        }
        protected override int GetEntityId(DocumentoLicencia entity)
        {
            return entity.Id;
        }
    }
}
