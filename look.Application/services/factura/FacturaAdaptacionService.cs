using look.Application.interfaces.factura;
using look.domain.entities.Common;
using look.domain.entities.factura;
using look.domain.interfaces;
using look.domain.interfaces.factura;
using Serilog;

namespace look.Application.services.factura
{
    public class FacturaAdaptacionService : Service<FacturaAdaptacion>, IFacturaAdaptacionService
    {
        private readonly IFacturaAdaptacionRepository _facturaAdaptacionService;
        private readonly ILogger _logger = Logger.GetLogger();
        public FacturaAdaptacionService(IFacturaAdaptacionRepository repository) : base(repository)
        {
            _facturaAdaptacionService = repository;
        }

        public async Task<FacturaAdaptacion> GetAllByIdHoras(int id)
        {
            try
            {
                _logger.Information("Consultando factura adaptación por horas utilizadas con id: {id}...");
                return await _facturaAdaptacionService.GetAllByIdHoras(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e);
                return null;
            }
        }

        public async Task<FacturaAdaptacion> GetAllByIdSoporte(int id)
        {
            try
            {
                _logger.Information("Consultando factura adaptacion por soporte con id: ${id}...");
                var result = await _facturaAdaptacionService.GetAllByIdSoporte(id);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor, e);
                return null;
            }
        }

        public async Task<FacturaAdaptacion> GetAllEntitiesByIdLicense(int id)
        {
            try
            {
                _logger.Information("Consultando factura adaptacion por licencia con id: {id}...");
                return await _facturaAdaptacionService.GetAllEntitiesByIdLicense(id);
            }
            catch (Exception)
            {
                _logger.Error(Message.ErrorServidor);
                return null;

            }
        }

        public async Task<FacturaAdaptacion> GetAllEntitiesByIdPeriod(int id)
        {
            try
            {
                _logger.Information("Consultando factura adaptacion por periodo con id: {id}...");
                return await _facturaAdaptacionService.GetAllEntitiesByIdPeriod(id);
            }
            catch (Exception)
            {
                _logger.Error(Message.ErrorServidor);
                return null;
            }
        }

        public async Task<FacturaAdaptacion> GetAllEntitiesByIdProyectoDesarrollo(int id)
        {
            try
            {
                _logger.Information("Consultando factura adaptacion por proyecto desarrollo con id: {id}...");
                return await _facturaAdaptacionService.GetAllEntitiesByIdProyectoDesarrollo(id);
            }
            catch (Exception)
            {
                _logger.Error(Message.ErrorServidor);
                return null;
            }
        }
    }
}
