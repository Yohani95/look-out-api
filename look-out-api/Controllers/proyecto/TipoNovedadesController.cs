using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoNovedadesController:BaseController<TipoNovedades>
    {
        private readonly ITipoNovedadesService _tipoTelefonoService;

        public TipoNovedadesController(ITipoNovedadesService service) : base(service)
        {
            _tipoTelefonoService = service;
        }

        protected override int GetEntityId(TipoNovedades entity)
        {
            return entity.id;
        }
    }
}

