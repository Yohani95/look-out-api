using look.Application.interfaces.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using Serilog;

namespace look.Application.services.proyecto
{
    public class TarifarioConvenioService: Service<TarifarioConvenio>, ITarifarioConvenioService
    {
    
        private readonly ITarifarioConvenioRepository _tarifarioConvenioService;
        private readonly ILogger _logger = Logger.GetLogger();

        public TarifarioConvenioService(ITarifarioConvenioRepository repository) : base(repository)
        {
            _tarifarioConvenioService = repository;
        }

        public async Task<ResponseGeneric<TarifarioConvenio>> GetByIdEntities(int id)
        {
            try
            {
                var tarifario = await _tarifarioConvenioService.GetAllAsync();
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TarifarioConvenio>> ListComplete()
        {
            return await _tarifarioConvenioService.GetComplete();
        }
    }
}

