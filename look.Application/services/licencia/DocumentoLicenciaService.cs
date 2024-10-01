using look.Application.interfaces.licencia;
using look.domain.entities.Common;
using look.domain.entities.licencia;
using look.domain.interfaces;
using look.domain.interfaces.licencia;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.licencia
{
    public class DocumentoLicenciaService : Service<DocumentoLicencia>, IDocumentoLicenciaService
    {
        private readonly IDocumentoLicenciaRepository _documentoLicenciaRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        public DocumentoLicenciaService(IDocumentoLicenciaRepository repository) : base(repository)
        {
            _documentoLicenciaRepository = repository;
        }

        public async Task<List<DocumentoLicencia>> GetByIdVentaLicencia(int id)
        {
            try
            {
                _logger.Information("Crear proyecto");
                return await _documentoLicenciaRepository.GetByIdVentaLicencia(id);
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor, ex);
                return null;
            }
        }
    }
}
