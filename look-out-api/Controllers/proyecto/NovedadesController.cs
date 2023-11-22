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
        
        [HttpPatch("NovedadesParticipante")]
        public async Task<IActionResult> novedadesParticipante(Novedades novedad)
        {
            var result = await _novedadesService.updateNovedad(novedad);

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
        
        
        private readonly INovedadesService _novedadesService;

        public NovedadesController(INovedadesService service) : base(service)
        {
            _novedadesService = service;
        }

        protected override int GetEntityId(Novedades entity)
        {
            return entity.id;
        }
    }
}

