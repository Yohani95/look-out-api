using look.Application.interfaces.soporte;
using look.domain.entities.Common;
using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using Serilog;


namespace look.Application.services.soporte
{
    public class HorasUtilizadasService : Service<HorasUtilizadas>, IHorasUtilizadasService
    {
        private readonly IHorasUtilizadasRepository _horasUtilizadasRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        public HorasUtilizadasService(IHorasUtilizadasRepository repository) : base(repository)
        {
            _horasUtilizadasRepository = repository;
        }

        public async Task<List<HorasUtilizadas>> getAllHorasByIdSoporte(int id)
        {
            try
            {
                return await _horasUtilizadasRepository.getAllHorasByIdSoporte(id);
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor+ex.Message);
                throw;
            }
        }
    }
}
