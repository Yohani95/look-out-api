using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.dto.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace look_out_api.Controllers.admin
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController :BaseController<Email>
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService service) : base(service)
        {
            _emailService = service;
        }

        [HttpGet("getAllEmail")]
        public async Task<IActionResult> GetAllEmail()
        {
            Log.Information("Solicitud GetAll email");
            var email = await _emailService.ListComplete();
            return Ok(email);
        }



        protected override int GetEntityId(Email entity)
        {
            return entity.EmaId;
        }
    }
}