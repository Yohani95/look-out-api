using look.Application.interfaces.admin;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoParticipanteController : BaseController<ProyectoParticipante>
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
        [HttpDelete("deletedAsync/{rut}")]
        public async Task<ActionResult<ServiceResult>> deletedAsync(string rut)
        {
            var result = await _participanteService.deletedAsync(rut);

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
        [HttpGet("GetByIdProyecto/{id}")]
        public async Task<ActionResult<IEnumerable<ProyectoParticipante>>> GetAllWithEntities(int id)
        {
            var result = await _participanteService.GetByIdProyecto(id);

            switch (result.serviceResult.MessageCode)
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
        [HttpPost("GetAllProfessionalsByIdProject")]
        public async Task<ActionResult<IEnumerable<PeriodoProfesionales>>> GetAllEntitiesByIdProject([FromBody] List<int> ids)
        {
            Log.Information("[GetAllEntitiesByIdProject] Solicitud post getall profesionales periodos con IDs: {Ids}", ids);

            if (ids == null || !ids.Any())
            {
                return BadRequest("La lista de IDs no puede estar vacía.");
            }

            var data = await _participanteService.GetAllEntitiesByIdsProject(ids);
            return Ok(data);
        }



        protected override int GetEntityId(ProyectoParticipante entity)
        {
            return entity.PpaId;
        }
    }
}


