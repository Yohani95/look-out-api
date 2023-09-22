using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces;
using look.domain.interfaces.admin;

namespace look.Application.services.admin
{
    public class PerfilService : Service<Perfil>, IPerfilService
    {
        //instanciar repository si se requiere 
        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository) : base(perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }
    }
}
