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
        public DocumentoOportunidadService(IDocumentoOportunidadRepository repository) : base(repository)
        {
        }
    }
}
