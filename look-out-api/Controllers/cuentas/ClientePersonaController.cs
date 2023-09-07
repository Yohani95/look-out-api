using look.Application.interfaces;
using look.Application.interfaces.cuentas;
using look.Application.services.cuentas;
using look.domain.dto.admin;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.cuentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientePersonaController : BaseController<ClientePersona>
    {
        private readonly IClientePersonaService _clientePersonaService;
        public ClientePersonaController(IClientePersonaService service) : base(service)
        {
            _clientePersonaService = service;
        }
        [HttpGet("GetAllClientRelations")]
        public async Task<ActionResult<ResponseGeneric<List<ClientePersona>>>> GetAllClientRelations()
        {
            Log.Information("Solicitud Get ClientePersona");

            var result =await _clientePersonaService.GetAllClientRelations();
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

        [HttpGet("GetPersonaDTOById/{id}")]
        public async Task<ActionResult<ResponseGeneric<List<PersonaDTO>>>> GetPersonaDTOById(int id)
        {
            Log.Information("Solicitud Get ClientePersona ID:" +id);

            var result = await _clientePersonaService.GetPersonaDTOById(id);
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


        protected override int GetEntityId(ClientePersona entity)
        {
            return entity.MyRowId;
        }
    }
}
