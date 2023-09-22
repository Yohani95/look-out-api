using look.Application.interfaces.admin;
using look.Application.services.admin;
using look.domain.entities.admin;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : BaseController<Perfil>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IPerfilService _perfilService;
        public PerfilController(IPerfilService perfilService) : base(perfilService)
        {
            _perfilService = perfilService;
        }

        protected override int GetEntityId(Perfil entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Perfil
            return entity.Id;
        }
    }
}
