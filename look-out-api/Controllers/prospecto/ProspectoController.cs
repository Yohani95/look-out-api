using look.Application.interfaces.prospecto;
using look.Application.services.admin;
using look.domain.entities.Common;
using look.domain.entities.prospecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;


namespace look_out_api.Controllers.prospecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProspectoController : BaseController<Prospecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IProspectoService _prospectoService;
        public ProspectoController(IProspectoService prospectoService) : base(prospectoService)
        {
            _prospectoService = prospectoService;
        }

        protected override int GetEntityId(Prospecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Prospecto
            return entity.Id;
        }

        [HttpPost("CargaMasiva")]
        public async Task<IActionResult> CargaMasiva(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se proporcionó un archivo o el archivo está vacío.");
            }

            var result = await _prospectoService.ProcesarCargaMasiva(file);

            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
