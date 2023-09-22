using look.Application.interfaces.proyecto;
using look.Application.services.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;


namespace look_out_api.Controllers.proyecto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProspectoController : BaseController<Prospecto>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IProspectoService _prospectoService;
        public ProspectoController(IProspectoService prospectoService) : base(prospectoService)
        {
            _prospectoService = prospectoService;
        }

        protected override int GetEntityId(Prospecto entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Prospecto
            return entity.PrsId;
        }
    }
}
