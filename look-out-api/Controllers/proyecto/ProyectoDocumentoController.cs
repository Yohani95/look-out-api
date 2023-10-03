using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoDocumentoController:BaseController<ProyectoDocumento>
    {
        private readonly IProyectoDocumentoService _proyectoService;

        public ProyectoDocumentoController(IProyectoDocumentoService service) : base(service)
        {
            _proyectoService = service;
        }
        
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<ProyectoDocumento>>> GetAllWithEntities()
        {
            var proyectosDocumentos = await _proyectoService.ListComplete();

            if (proyectosDocumentos == null)
            {
                return NotFound();
            }

            return proyectosDocumentos;
        }
        [HttpGet("GetByIdProject/{id}")]
        public async Task<ActionResult<IEnumerable<ProyectoDocumento>>> GetByIdProject(int id)
        {
            var proyectosDocumentos = await _proyectoService.GetByIdProject(id);

            if (proyectosDocumentos == null)
            {
                return NotFound();
            }

            return proyectosDocumentos;
        }

        protected override int GetEntityId(ProyectoDocumento entity)
        {
            return entity.PydId;
        }
    }
}

