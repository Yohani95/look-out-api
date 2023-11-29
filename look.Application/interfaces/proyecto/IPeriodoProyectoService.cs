using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface IPeriodoProyectoService: IService<PeriodoProyecto>
    {
        Task<List<PeriodoProyecto>> ListComplete();
        Task<List<PeriodoProyecto>> ListByProyecto(int id);
    }
}

