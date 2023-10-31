using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.entities.Common;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoController: BaseController<Telefono>
    {
        private readonly ITelefonoService _telefonoService;

        public TelefonoController(ITelefonoService service) : base(service)
        {
            _telefonoService = service;
        }
        
        [HttpGet("getAllTelefonos")]
        public async Task<IActionResult> GetAllTelefono()
        {
            Log.Information("Solicitud GetAll telefono");
            var email = await _telefonoService.ListComplete();
            return Ok(email);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateTelefono(Telefono telefono)
        {
            Log.Information("Solicitud Create telefono");
            var result = await _telefonoService.Create(telefono);
            switch (result.MessageCode)
            {
                case ServiceResultMessage.Success:
                    return Ok(result);
                case ServiceResultMessage.InvalidInput:
                    return BadRequest(result);
                case ServiceResultMessage.NotFound:
                    return NotFound(result);
                case ServiceResultMessage.Conflict:
                    return UnprocessableEntity(result);
                default:
                    return StatusCode(500, result);
            }
        }
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(Telefono telefono,int id)
        {
            Log.Information("Solicitud Create telefono");
            var result = await _telefonoService.Edit(telefono,id);
            switch (result.MessageCode)
            {
                case ServiceResultMessage.Success:
                    return Ok(result);
                case ServiceResultMessage.InvalidInput:
                    return BadRequest(result);
                case ServiceResultMessage.NotFound:
                    return NotFound(result);
                case ServiceResultMessage.Conflict:
                    return UnprocessableEntity(result);
                default:
                    return StatusCode(500, result);
            }
        }
        
        [HttpGet("getAllTelefonoById/{id}")]
        public async Task<IActionResult> getAllTelefonoById(int id)
        {
            Log.Information("Solicitud GetAll email");
            var email = await _telefonoService.ListCompleteById(id);
            return Ok(email);
        }


        protected override int GetEntityId(Telefono entity)
        {
            return entity.telId;
        }
    }
}

