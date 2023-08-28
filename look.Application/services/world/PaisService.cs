using look.Application.interfaces.world;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;


namespace look.Application.services.world
{
    public class PaisService : Service<Pais>, IPaisService
    {
        //intanciar repository si se requiere 
        private readonly IPaisRepository _paisRepository;

        public PaisService(IPaisRepository paisRepository) : base(paisRepository)
        {
            _paisRepository = paisRepository;
        }
    }
}
