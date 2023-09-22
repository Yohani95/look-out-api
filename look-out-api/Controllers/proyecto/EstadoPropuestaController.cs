using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPropuestaController : BaseController<EstadoPropuesta>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IEstadoPropuestaService _estadoPropuestaService;
        public EstadoPropuestaController(IEstadoPropuestaService estadoPropuestaService) : base(estadoPropuestaService)
        {
            _estadoPropuestaService = estadoPropuestaService;
        }

        protected override int GetEntityId(EstadoPropuesta entity)
        {
            // Implementa la lógica para obtener el ID de la entidad EstadoPropuesta
            return entity.EppId;
        }
    }
}
