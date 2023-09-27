using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class ProyectoDocumentoService: Service<ProyectoDocumento>, IProyectoDocumentoService
    {
    
        private readonly IProyectoDocumentoRepository _proyectoDocumentoRepository;
    
        public ProyectoDocumentoService(IProyectoDocumentoRepository repository) : base(repository)
        {
            _proyectoDocumentoRepository = repository;
        }
    
        public async Task<List<ProyectoDocumento>> ListComplete()
        {
            return await _proyectoDocumentoRepository.GetComplete();
        }
    }
}


    
