using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : BaseController<Propuesta>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IPropuestaService _propuestaService;
        public PropuestaController(IPropuestaService propuestaService) : base(propuestaService)
        {
            _propuestaService = propuestaService;
        }

        protected override int GetEntityId(Propuesta entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Propuesta
            return entity.PrpId;
        }
    }
}
