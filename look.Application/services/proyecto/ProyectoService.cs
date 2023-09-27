using look.Application.interfaces.admin;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class ProyectoService: Service<Proyecto>, IProyectoService
    {
    
        private readonly IProyectoRepository _proyectoRepository;
    
        public ProyectoService(IProyectoRepository repository) : base(repository)
        {
            _proyectoRepository = repository;
        }
    
        public async Task<List<Proyecto>> ListComplete()
        {
            return await _proyectoRepository.GetComplete();
        }
    }
}

