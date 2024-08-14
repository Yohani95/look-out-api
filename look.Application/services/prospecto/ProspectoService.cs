using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.prospecto
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
