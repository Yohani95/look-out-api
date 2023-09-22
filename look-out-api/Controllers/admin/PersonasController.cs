using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.dto.admin;
using look.domain.entities.admin;
using look.domain.entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : BaseController<Persona>
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService service) : base(service)
        {
            _personaService = service;
        }
        [HttpGet("tipoPersona/{id}")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonaByKam(int id)
        {
            var persona = await _personaService.GetAllByType(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }
        [HttpPost("createWithEntities")]
        public async Task<IActionResult> Create(PersonaDTO personaDTO)
        {
            var result=await  _personaService.Create(personaDTO);
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
        [HttpPut("editWithEntities/{id}")]
        public async Task<IActionResult> Edit(int id,PersonaDTO personaDTO)
        {
            var result = await _personaService.Edit(id,personaDTO);
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
        [HttpDelete("deleteWithEntities/{id}")]
        public async Task<ActionResult<ServiceResult>> DeleteEntities(int id)
        {
            var result = await _personaService.Delete(id);
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
        [HttpGet("GetAllContactEnteties")]
        public async Task<ActionResult<ResponseGeneric<List<PersonaDTO>>>> GetAllContactEnteties()
        {
            var result = await _personaService.GetAllContactEnteties();

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
        [HttpGet("GetAllContactByIdClient/{id}")]
        public async Task<ActionResult<ResponseGeneric<List<Persona>>>> GetAllContactByIdClient(int id)
        {
            var result = await _personaService.GetAllContactByIdClient(id);

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
        protected override int GetEntityId(Persona entity)
        {
           return entity.Id;
        }
    }
}
