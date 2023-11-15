using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class PeriodoProyectoService: Service<PeriodoProyectos>, IPeriodoProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IPeriodoProyectoRepository _periodoProyectoRepository;

        public PeriodoProyectoService(IPeriodoProyectoRepository periodoProyectoRepository) : base(periodoProyectoRepository)
        {
            _periodoProyectoRepository = periodoProyectoRepository;
        }
        public async Task<List<PeriodoProyectos>> ListComplete()
        {
            return await _periodoProyectoRepository.GetComplete();
        }
    }
}

