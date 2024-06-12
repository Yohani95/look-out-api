using look.Application.interfaces.oportunidad;
using look.Application.interfaces.proyecto;
using look.domain.entities.oportunidad;
using look.domain.interfaces;
using look.domain.interfaces.oportunidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.oportunidad
{
    public class DocumentoOportunidadService : Service<DocumentoOportunidad>, IDocumentoOportunidadService
    {
        private readonly IDocumentoOportunidadRepository _documento;
        public DocumentoOportunidadService(IDocumentoOportunidadRepository repository) : base(repository)
        {
            _documento = repository;
        }

        public async Task<List<DocumentoOportunidad>> GetByIdOportunidad(int id)
        {
            try
            {
                return await _documento.GetByidOportunidad(id);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
