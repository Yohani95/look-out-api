using look.Application.interfaces.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController:BaseController<Proyecto>
    {
        private readonly IProyectoService _proyectoService;

        public ProyectoController(IProyectoService service) : base(service)
        {
            _proyectoService = service;
        }
        
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetAllWithEntities()
        {
            var proyectos = await _proyectoService.ListComplete();

            if (proyectos == null)
            {
                return NotFound();
            }

            return proyectos;
        }

        protected override int GetEntityId(Proyecto entity)
        {
            return entity.PryId;
        }
    }
}


