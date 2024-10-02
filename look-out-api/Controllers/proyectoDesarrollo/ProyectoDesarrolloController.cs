using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoDesarrolloController : BaseController<ProyectoDesarrollo>
    {
        public ProyectoDesarrolloController(IProyectoDesarrolloService service) : base(service)
        {
        }

        protected override int GetEntityId(ProyectoDesarrollo entity)
        {
            return entity.Id;
        }
    }
}
