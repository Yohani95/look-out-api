using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        protected override int GetEntityId(Email entity)
        {
            throw new NotImplementedException();
        }
    }
}
