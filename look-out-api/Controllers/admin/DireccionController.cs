using look.Application.interfaces.admin;
using look.domain.entities.Common;
using look.domain.entities.world;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.admin
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController: BaseController<Direccion>
    {
        private readonly IDireccionService _direccionService;

        public DireccionController(IDireccionService service) : base(service)
        {
            _direccionService = service;
        }
        
        [HttpGet("getAllDireccion")]
        public async Task<IActionResult> GetAllDireccion()
        {
            Log.Information("Solicitud GetAll Direccion");
            var email = await _direccionService.ListComplete();
            return Ok(email);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateEmail(Direccion telefono)
        {
            Log.Information("Solicitud Create email");
            var result = await _direccionService.Create(telefono);
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
        public async Task<IActionResult> Edit(Direccion direccion,int id)
        {
            Log.Information("Solicitud Create direccion");
            var result = await _direccionService.Edit(direccion,id);
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
        protected override int GetEntityId(Direccion entity)
        {
            return entity.DirId;
        }
    }
    
}

