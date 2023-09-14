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

        protected override int GetEntityId(TipoEmail entity)
        {
            return entity.temId;
        }
    }
}
