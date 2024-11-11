using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoProyectoDesarrolloController : BaseController<DocumentoProyectoDesarrollo>
    {
        private readonly IDocumentoProyectoDesarrolloService _service;
        public DocumentoProyectoDesarrolloController(IDocumentoProyectoDesarrolloService service) : base(service)
        {
            _service = service;
        }

        protected override int GetEntityId(DocumentoProyectoDesarrollo entity)
        {
            return entity.Id;
        }
        [HttpGet("GetByidProyectoDesarrollo/{id}")]
        public async Task<IActionResult> GetByidProyectoDesarrollo(int id)
        {
            var result = await _service.GetByidProyectoDesarrollo(id);
            return Ok(result);
        }
    }
}
