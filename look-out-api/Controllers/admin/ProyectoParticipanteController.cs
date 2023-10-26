using look.Application.interfaces.admin;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoParticipanteController:BaseController<ProyectoParticipante>
    {
        private readonly IProyectoParticipanteService _participanteService;

        public ProyectoParticipanteController(IProyectoParticipanteService service) : base(service)
        {
            _participanteService = service;
        }
        
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<ProyectoParticipante>>> GetAllWithEntities()
        {
            var proyectosParticipantes = await _participanteService.ListComplete();

            if (proyectosParticipantes == null)
            {
                return NotFound();
            }

            return proyectosParticipantes;
        }
        [HttpPost("CreateAsync")]
        public async Task<ActionResult<ServiceResult>> CreateDTOAsync(ProfesionalesDTO profesionales)
        {
            //TEST
            return Ok();
            var result = await _participanteService.CreateDTOAsync(profesionales);

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
        protected override int GetEntityId(ProyectoParticipante entity)
        {
            return entity.PpaId;
        }
    }
}


