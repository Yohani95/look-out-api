using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoHitoPRoyectoDesarrolloController : BaseController<TipoHitoProyectoDesarrollo>
    {
        public TipoHitoPRoyectoDesarrolloController(ITipoHitoProyectoDesarrolloService service) : base(service)
        {
        }

        protected override int GetEntityId(TipoHitoProyectoDesarrollo entity)
        {
            return entity.Id;
        }
    }
}
