using look.Application.interfaces;
using look.Application.interfaces.cuentas;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.cuentas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : BaseController<Cliente>
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService service) : base(service)
        {
            _clienteService = service;
        }
        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllWithEntities()
        {
            var persona = await _clienteService.GetAllWithEntities();

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }
        [HttpPost("CreateWithEntities")]
        public async Task<IActionResult> CreateWithEntities(ClienteWithIds clienteWithIds)
        {
            Log.Information(clienteWithIds.ToString());
            var result = await _clienteService.CreateWithEntities(clienteWithIds.Cliente, clienteWithIds.IdPerson,(int)clienteWithIds.kamIdPerson.Id);

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
        [HttpPut("UpdateWithEntities/{id}")]
        public async Task<IActionResult> UpdateWithEntities(int id,ClienteWithIds clienteWithIds)
        {
            Log.Information("Solicitud Delete ClienteId: " + id);
            var result = await _clienteService.EditWithEntities(id,clienteWithIds.Cliente, clienteWithIds.IdPerson,(int)clienteWithIds.kamIdPerson.Id);

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
        [HttpDelete("DeleteWithEntities/{id}")]
        public async Task<IActionResult> DeleteWithEntities(int id)
        {
            Log.Information("Solicitud Delete ClienteId: "+id);
            var client = await _clienteService.GetByIdAsync(id);
            if(client == null)
            {
                return NotFound(id);
            }
            var result = await _clienteService.DeleteWithEntities(client);

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

        [HttpGet("GetAllIdWithContact/{clientId}")]
        public async Task<IActionResult> GetAllIdWithContact(int clientId)
        {
            ResponseGeneric<List<int>> response = await _clienteService.GetAllIdWithContact(clientId);

            if (!response.serviceResult.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("GetByIdWithKamAndContact/{clientId}")]
        public async Task<IActionResult> GetByIdWithKamAndContact(int clientId)
        {
            ResponseGeneric<ClienteWithIds> response = await _clienteService.GetByIdWithKamAndContact(clientId);

            if (!response.serviceResult.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        protected override int GetEntityId(Cliente entity)
        {
            return entity.CliId;
        }
    }
}
