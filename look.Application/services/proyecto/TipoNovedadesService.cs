using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    
    public class TipoNovedadesService: Service<TipoNovedades>, ITipoNovedadesService
    {
        private readonly ITipoNovedadesRepository _tipoNovedadesRepository;
        
        public TipoNovedadesService(ITipoNovedadesRepository repository) : base(repository)
        {
            _tipoNovedadesRepository = repository;
        }

        public async Task<List<TipoNovedades>> ListComplete()
        {
            return await _tipoNovedadesRepository.GetComplete();
        }
    }
}

