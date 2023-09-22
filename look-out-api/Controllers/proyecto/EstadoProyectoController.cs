using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.Application.services.proyecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoProyectoController : BaseController<EstadoProyecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IEstadoProyectoService _estadoProyectoService;
        public EstadoProyectoController(IEstadoProyectoService estadoProyectoService) : base(estadoProyectoService)
        {
            _estadoProyectoService = estadoProyectoService;
        }

        protected override int GetEntityId(EstadoProyecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad EstadoProyecto
            return entity.EpyId;
        }
}
}
