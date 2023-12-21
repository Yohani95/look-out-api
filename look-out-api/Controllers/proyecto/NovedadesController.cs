using look.Application.interfaces.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadesController:BaseController<Novedades>
    {   
        
        private readonly INovedadesService _novedadesService;

        public NovedadesController(INovedadesService service) : base(service)
        {
            _novedadesService = service;
        }

        protected override int GetEntityId(Novedades entity)
        {
            return entity.id;
        }

        [HttpPut("updateNovedadesParticipante/{id}")]
        public async Task<IActionResult> novedadesParticipante(Novedades novedad,int id)
        {
            var result = await _novedadesService.updateNovedad(novedad,id);

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
        [HttpGet("NovedadesWithEntities")]
        public async Task<ActionResult<Novedades>> NovedadesWithEntities()
        {
            var result = await _novedadesService.ListComplete();
            return Ok(result);
        }
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(Novedades novedad)
        {
            var result = await _novedadesService.createNovedad(novedad);

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
    }
}

