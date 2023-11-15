using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface IPeriodoProyectoService: IService<PeriodoProyectos>
    {
        Task<List<PeriodoProyectos>> ListComplete();
    }
}

