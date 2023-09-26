using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : BaseController<Proyecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IProyectoService _proyectoService;
        public ProyectoController(IProyectoService proyectoService) : base(proyectoService)
        {
            _proyectoService = proyectoService;
        }

        protected override int GetEntityId(Proyecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Proyecto
            return entity.PryId;
        }

        [HttpGet("GetLastId")]
        public async Task<IActionResult> GetLastId() 
        {           
            var result = await _proyectoService.GetLastId();
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

        [HttpPost("createAsync")]
        public async Task<IActionResult> CreateAsync(IFormFile file1 ,IFormFile file2,Proyecto proyecto)
        {
            var result = await _proyectoService.createAsync(file1,file2,proyecto);
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
