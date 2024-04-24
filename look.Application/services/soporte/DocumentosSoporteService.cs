using look.Application.interfaces.soporte;
using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.soporte
{
    public class DocumentosSoporteService : Service<DocumentosSoporte>, IDocumentosSoporteService
    {
        public DocumentosSoporteService(IDocumentosSoporteRepository repository) : base(repository)
        {
        }
    }
}
