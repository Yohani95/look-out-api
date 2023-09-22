using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class EstadoProyectoService : Service<EstadoProyecto>, IEstadoProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IEstadoProyectoRepository _estadoProyectoRepository;

        public EstadoProyectoService(IEstadoProyectoRepository estadoProyectoRepository) : base(estadoProyectoRepository)
        {
            _estadoProyectoRepository = estadoProyectoRepository;
        }
    }
}
