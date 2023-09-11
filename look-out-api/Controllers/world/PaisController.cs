using look.Application.interfaces.world;
using look.Application.services.admin;
using look.domain.entities.world;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace look_out_api.Controllers.world
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : BaseController<Pais>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IPaisService _paisService;
        public PaisController(IPaisService paisService) : base(paisService)
        {
            _paisService = paisService;
        }

        protected override int GetEntityId(Pais entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Pais
            return entity.PaiId; 
        }
    }
}
