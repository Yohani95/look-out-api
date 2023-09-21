using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;


namespace look.Application.services.proyecto
{
    public class DocumentoService : Service<Documento>, IDocumentoService
    {
        //instanciar repository si se requiere 
        private readonly IDocumentoRepository _documentoRepository;

        public DocumentoService(IDocumentoRepository documentoRepository) : base(documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }
    }
}
