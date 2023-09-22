using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class ProspectoService : Service<Prospecto>, IProspectoService
    {
        //instanciar repository si se requiere 
        private readonly IProspectoRepository _prospectoRepository;

        public ProspectoService(IProspectoRepository prospectoRepository) : base(prospectoRepository)
        {
            _prospectoRepository = prospectoRepository;
        }
    }
}
