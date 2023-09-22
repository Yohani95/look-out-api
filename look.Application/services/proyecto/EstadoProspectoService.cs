using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class EstadoProspectoService : Service<EstadoProspecto>, IEstadoProspectoService
    {
        //instanciar repository si se requiere 
        private readonly IEstadoProspectoRepository _estadoProspectoRepository;

        public EstadoProspectoService(IEstadoProspectoRepository estadoProspectoRepository) : base(estadoProspectoRepository)
        {
            _estadoProspectoRepository = estadoProspectoRepository;
        }
    }
}
