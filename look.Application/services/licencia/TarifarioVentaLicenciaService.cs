using look.Application.interfaces.licencia;
using look.domain.entities.Common;
using look.domain.entities.licencia;
using look.domain.interfaces;
using look.domain.interfaces.licencia;
using look.domain.interfaces.proyecto;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.licencia
{
    public class TarifarioVentaLicenciaService : Service<TarifarioVentaLicencia>, ITarifarioVentaLicenciaService
    {
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly ITarifarioVentaLicenciaRepository _repository;
        public TarifarioVentaLicenciaService(ITarifarioVentaLicenciaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TarifarioVentaLicencia>> GetAllEntitiesByIdLicense(int idLicencia)
        {
            try
            {
                return await _repository.GetAllEntitiesByIdLicense(idLicencia);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor + e.Message);
                return null;
            }
        }
    }
}
