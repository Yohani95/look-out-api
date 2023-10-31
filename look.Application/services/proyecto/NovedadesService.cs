using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    
    public class NovedadesService: Service<Novedades>, INovedadesService
    {
        private readonly INovedadesRepository _novedadesRepository;
        
        public NovedadesService(INovedadesRepository repository) : base(repository)
        {
            _novedadesRepository = repository;
        }

        public async Task<List<Novedades>> ListComplete()
        {
            return await _novedadesRepository.GetComplete();
        }
    }
}
