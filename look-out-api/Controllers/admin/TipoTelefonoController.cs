using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoTelefonoController:BaseController<TipoTelefono>
    {
        private readonly ITipoTelefonoService _tipoTelefonoService;

        public TipoTelefonoController(ITipoTelefonoService service) : base(service)
        {
            _tipoTelefonoService = service;
        }

        [HttpGet("getAllEmail")]
        public async Task<IActionResult> GetAllEmail()
        {
            Log.Information("Solicitud GetAll email");
            var email = await _tipoTelefonoService.ListComplete();
            return Ok(email);
        }



        protected override int GetEntityId(TipoTelefono entity)
        {
            return entity.tteId;
        }
    }
}

