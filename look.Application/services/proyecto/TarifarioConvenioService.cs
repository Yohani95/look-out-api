using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class TarifarioConvenioService: Service<TarifarioConvenio>, ITarifarioConvenioService
    {
    
        private readonly ITarifarioConvenioRepository _tarifarioConvenioService;
    
        public TarifarioConvenioService(ITarifarioConvenioRepository repository) : base(repository)
        {
            _tarifarioConvenioService = repository;
        }
    
        public async Task<List<TarifarioConvenio>> ListComplete()
        {
            return await _tarifarioConvenioService.GetComplete();
        }
    }
}

