using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadesController:BaseController<Novedades>
    {
        private readonly INovedadesService _novedadesService;

        public NovedadesController(INovedadesService service) : base(service)
        {
            _novedadesService = service;
        }

        protected override int GetEntityId(Novedades entity)
        {
            return entity.id;
        }
    }
}

