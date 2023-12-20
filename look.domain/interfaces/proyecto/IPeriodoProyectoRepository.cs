using look.domain.entities.admin;
using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface IPeriodoProyectoRepository: IRepository<PeriodoProyecto>
    {
        Task<List<PeriodoProyecto>> GetComplete();
    }
}

