using look.Application.interfaces.world;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;


namespace look.Application.services.world
{
    public class MonedaService : Service<Moneda>, IMonedaService
    {
        //instanciar repository si se requiere 
        private readonly IMonedaRepository _monedaRepository;

        public MonedaService(IMonedaRepository monedaRepository) : base(monedaRepository)
        {
            _monedaRepository = monedaRepository;
        }
    }
}
