using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.dto.admin;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace look_out_api.Controllers.admin
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : BaseController<Email>
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

        [HttpGet("getAllEmailById/{id}")]
        public async Task<IActionResult> getAllEmailById(int id)
        {
            Log.Information("Solicitud GetAll email");
            var email = await _emailService.ListCompleteById(id);
            return Ok(email);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateEmail(Email email)
        {
            Log.Information("Solicitud Create email");
            var result = await _emailService.Create(email);
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
        public async Task<IActionResult> Edit(Email email, int id)
        {
            Log.Information("Solicitud Create email");
            var result = await _emailService.Edit(email, id);
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
        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail(EmailRequest emailRequest)
        {
            try
            {
                await _emailService.SendEmailAsync(emailRequest.ToName, emailRequest.ToEmail, emailRequest.Subject, emailRequest.Body);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("enviar-email-factura")]
        public async Task<IActionResult> EnviarEmailFactura(PeriodoProyectoDto periodoProyectoDto)
        {
            if (periodoProyectoDto == null)
            {
                return BadRequest("Los datos enviados son inválidos.");
            }

            await _emailService.EnviarEmailSuporvisorFacturas(periodoProyectoDto);
            return Ok("Correo enviado exitosamente.");
        }
        public class EmailRequest
        {
            public string ToName { get; set; }
            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
        protected override int GetEntityId(Email entity)
        {
            return entity.EmaId;
        }
    }
}