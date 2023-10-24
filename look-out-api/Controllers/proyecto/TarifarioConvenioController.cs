using look.Application.interfaces.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TarifarioConvenioController:BaseController<TarifarioConvenio>
    {
        private readonly ITarifarioConvenioService _tarifarioConvenioService;

        public TarifarioConvenioController(ITarifarioConvenioService service) : base(service)
        {
            _tarifarioConvenioService = service;
        }
        
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<TarifarioConvenio>>> GetAllWithEntities()
        {
            var proyectosParticipantes = await _tarifarioConvenioService.ListComplete();

            if (proyectosParticipantes == null)
            {
                return NotFound();
            }

            return proyectosParticipantes;
        }
        [HttpGet("GetByIdProyectoWithEntities/{id}")]
        public async Task<ActionResult<IEnumerable<TarifarioConvenio>>> GetByIdProyectoWithEntities(int id)
        {
            var result = await _tarifarioConvenioService.GetByIdProyectoEntities(id);
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
        [HttpGet("GetByIdWithEntities/{id}")]
        public async Task<ActionResult<IEnumerable<TarifarioConvenio>>> GetByIdWithEntities(int id)
        {
            var result = await _tarifarioConvenioService.GetByIdEntities(id);
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


        protected override int GetEntityId(TarifarioConvenio entity)
        {
            return entity.TcId;
        }
    }
}
