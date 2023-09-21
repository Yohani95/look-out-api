using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class TipoDocumentoService : Service<TipoDocumento>, ITipoDocumentoService
    {
        //instanciar repository si se requiere 
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public TipoDocumentoService(ITipoDocumentoRepository tipoDocumentoRepository) : base(tipoDocumentoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }
    }
}
