using look.Application.interfaces.admin;
using look.domain.entities.admin;
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
        public async Task<IActionResult> GetAllEmail()
        {
            Log.Information("Solicitud GetAll email");
            var email = await _telefonoService.ListComplete();
            return Ok(email);
        }


        protected override int GetEntityId(Telefono entity)
        {
            return entity.telId;
        }
    }
}

