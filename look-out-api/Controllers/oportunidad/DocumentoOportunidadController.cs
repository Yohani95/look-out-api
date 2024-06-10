using look.Application.interfaces;
using look.Application.interfaces.oportunidad;
using look.domain.entities.Common;
using look.domain.entities.oportunidad;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.oportunidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoOportunidadController : BaseController<DocumentoOportunidad>
    {
        private readonly IDocumentoOportunidadService _documentoOportunidadService;
        public DocumentoOportunidadController(IDocumentoOportunidadService service) : base(service)
        {
            _documentoOportunidadService = service;
        }
        [HttpGet("getbyIdOportunidad/{id}")]
        public async Task<IActionResult> GetLastId(int id)
        {
            var result = await _documentoOportunidadService.GetByIdOportunidad(id);
                return Ok(result);
        }
        protected override int GetEntityId(DocumentoOportunidad entity)
        {
            return  entity.Id;
        }
    }
}
