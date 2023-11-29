using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;

namespace look.Application.services.proyecto
{
    public class PeriodoProyectoService: Service<PeriodoProyecto>, IPeriodoProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IPeriodoProyectoRepository _periodoProyectoRepository;

        public PeriodoProyectoService(IPeriodoProyectoRepository periodoProyectoRepository) : base(periodoProyectoRepository)
        {
            _periodoProyectoRepository = periodoProyectoRepository;
        }

        public async Task<List<PeriodoProyecto>> ListByProyecto(int id)
        {
            var periodos= await _periodoProyectoRepository.GetComplete();
            return periodos.Where(p=>p.PryId==id).ToList();
        }

        public async Task<List<PeriodoProyecto>> ListComplete()
        {
            return await _periodoProyectoRepository.GetComplete();
        }
    }
}

