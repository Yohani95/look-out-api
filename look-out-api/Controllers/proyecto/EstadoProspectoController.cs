using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoProspectoController : BaseController<EstadoProspecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IEstadoProspectoService _estadoProspectoService;
        public EstadoProspectoController(IEstadoProspectoService estadoProspectoService) : base(estadoProspectoService)
        {
            _estadoProspectoService = estadoProspectoService;
        }

        protected override int GetEntityId(EstadoProspecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad EstadoProspecto
            return entity.EpsId;
        }
}
}
