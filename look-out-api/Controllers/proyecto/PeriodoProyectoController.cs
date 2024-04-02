using look.Application.interfaces.proyecto;
using look.domain.dto.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoProyectoController: BaseController<PeriodoProyecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IPeriodoProyectoService _periodoProyectoService;
        
        public PeriodoProyectoController(IPeriodoProyectoService periodoProyectoService) : base(periodoProyectoService)
        {
            _periodoProyectoService = periodoProyectoService;
        }
        
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<PeriodoProyecto>>> GetAllWithEntities()
        {
            var proyectosDocumentos = await _periodoProyectoService.ListComplete();

            if (proyectosDocumentos == null)
            {
                return NotFound();
            }

            return proyectosDocumentos;
        }
        [HttpGet("GetByIdProyecto/{id}")]
        public async Task<ActionResult<IEnumerable<PeriodoProyecto>>> GetByIdProyecto(int id)
        {
            var periodo = await _periodoProyectoService.ListByProyecto(id);
            return Ok(periodo);
        }
        
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(PeriodoProyecto periodo)
        {
            var result = await _periodoProyectoService.CreateAsync(periodo);

            switch (result.MessageCode)
            {
                case ServiceResultMessage.Success:
                    return Ok(result);
                case ServiceResultMessage.InvalidInput:
                    return BadRequest(result);
                case ServiceResultMessage.NotFound:
                    return NotFound(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpGet("GetPeriodoProyecto/{id}")]
        public async Task<ActionResult<PeriodoProyecto>> GetPeriodoProyecto(int id)
        {
            var periodo = await _periodoProyectoService.GetPeriodoProyectoById(id);
            if(periodo == null)return NotFound($"no existe el periodo ID: {id}");
            return Ok(periodo);
        }
        protected override int GetEntityId(PeriodoProyecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad TipoDocumento
            return entity.id;
        }
        
    }
}

