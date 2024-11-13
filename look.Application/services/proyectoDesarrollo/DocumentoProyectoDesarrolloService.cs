using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.Common;
using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces;
using look.domain.interfaces.proyectoDesarrollo;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.proyectoDesarrollo
{
    internal class DocumentoProyectoDesarrolloService : Service<DocumentoProyectoDesarrollo>, IDocumentoProyectoDesarrolloService
    {
        private readonly IDocumentoProyectoDesarrolloRepository _repository;
        private readonly ILogger _logger = Logger.GetLogger();
        public DocumentoProyectoDesarrolloService(IDocumentoProyectoDesarrolloRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<DocumentoProyectoDesarrollo>> GetByidProyectoDesarrollo(int id)
        {
            try
            {
                return await _repository.GetByidProyectoDesarrollo(id);
            }
            catch (Exception e)
            {
                _logger.Error(Message.ErrorServidor, e.Message);
                return null;
            }
        }
    }
}
