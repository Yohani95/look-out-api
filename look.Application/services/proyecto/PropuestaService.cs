using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class PropuestaService : Service<Propuesta>, IPropuestaService
    {
        //instanciar repository si se requiere 
        private readonly IPropuestaRepository _propuestaRepository;

        public PropuestaService(IPropuestaRepository propuestaRepository) : base(propuestaRepository)
        {
            _propuestaRepository = propuestaRepository;
        }
    }
}
