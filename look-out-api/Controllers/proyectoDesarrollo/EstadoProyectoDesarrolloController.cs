using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.Application.services;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoProyectoDesarrolloController : BaseController<EstadoProyectoDesarrollo>
    {
        public EstadoProyectoDesarrolloController(IEstadoProyectoDesarrolloService service) : base(service)
        {
        }

        protected override int GetEntityId(EstadoProyectoDesarrollo entity)
        {
            return entity.Id;
        }
    }
}
