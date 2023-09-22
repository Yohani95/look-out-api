using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class EstadoPropuestaService : Service<EstadoPropuesta>, IEstadoPropuestaService
    {
        //instanciar repository si se requiere 
        private readonly IEstadoPropuestaRepository _estadoPropuestaRepository;

        public EstadoPropuestaService(IEstadoPropuestaRepository estadoPropuestaRepository) : base(estadoPropuestaRepository)
        {
            _estadoPropuestaRepository = estadoPropuestaRepository;
        }
    }
}
