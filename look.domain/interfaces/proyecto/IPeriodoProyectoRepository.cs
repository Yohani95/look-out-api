using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface IPeriodoProyectoRepository: IRepository<PeriodoProyectos>
    {
        Task<List<PeriodoProyectos>> GetComplete();
    }
}

