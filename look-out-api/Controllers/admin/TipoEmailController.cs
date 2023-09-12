using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEmailController:BaseController<TipoEmail>
    {
        private readonly ITipoEmailService _tipoEmailService;

        public TipoEmailController(ITipoEmailService service) : base(service)
        {
            _tipoEmailService = service;
        }

        [HttpGet("getAllEmail")]
        public async Task<IActionResult> GetAllEmail()
        {
            Log.Information("Solicitud GetAll email");
            var email = await _tipoEmailService.ListComplete();
            return Ok(email);
        }



        protected override int GetEntityId(TipoEmail entity)
        {
            return entity.temId;
        }
    }
}
