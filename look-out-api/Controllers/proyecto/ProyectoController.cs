using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : BaseController<Proyecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IProyectoService _proyectoService;
        public ProyectoController(IProyectoService proyectoService) : base(proyectoService)
        {
            _proyectoService = proyectoService;
        }

        protected override int GetEntityId(Proyecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Proyecto
            return entity.PryId;
        }
    }
}
