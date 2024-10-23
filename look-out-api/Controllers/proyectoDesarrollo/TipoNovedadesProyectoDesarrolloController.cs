using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoNovedadesProyectoDesarrolloController : BaseController<TipoNovedadProyectoDesarrollo>
    {
        public TipoNovedadesProyectoDesarrolloController(ITipoNovedadesProyectoDesarrolloService service) : base(service)
        {
        }

        protected override int GetEntityId(TipoNovedadProyectoDesarrollo entity)
        {
            return entity.Id;
        }
    }
}
