using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.dto.admin;
using look.domain.dto.proyecto;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

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
        public async Task<IActionResult> CreateAsync([FromForm] string proyectoJson,[FromForm] List<IFormFile> files)
        {
            
            var proyecto = JsonConvert.DeserializeObject<Proyecto>(proyectoJson);
            IFormFile file1;
            IFormFile file2;
            file1 = files[0];
            file2 = files[1];
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

        [HttpGet("GeFileByProject")]
        public IActionResult DescargarArchivo(string path)
        {
            if (System.IO.File.Exists(path))
            {
                var archivoStream = System.IO.File.OpenRead(path);
                return File(archivoStream, "application/octet-stream");
            }
            else
            {
                Log.Warning(Message.SinDocumentos);
                return NotFound();
            }
        }
        
        [HttpDelete("DeleteWithEntities/{id}")]
        public async Task<IActionResult> DeleteWithEntities(int id)
        {
            Log.Information("Solicitud Delete ProyectoId: " + id);
            var result = await _proyectoService.deleteAsync(id);

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
        public async Task<IActionResult> UpdateWithEntities([FromForm] string proyectoJson,[FromForm] List<IFormFile> files)
        {
            var proyecto = JsonConvert.DeserializeObject<Proyecto>(proyectoJson);
            IFormFile file1;
            IFormFile file2;
            file1 = files[0];
            file2 = files[1];
            Log.Information("Solicitud Delete ProyectoId: " + proyecto.PryId);
            var result = await _proyectoService.updateAsync(file1 ,file2 ,proyecto);

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

        [HttpGet("WithEntities")]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetAllWithEntities()
        {
            var proyectosDocumentos = await _proyectoService.GetComplete();

            if (proyectosDocumentos == null)
            {
                return NotFound();
            }

            return proyectosDocumentos;
        }
    }
}
