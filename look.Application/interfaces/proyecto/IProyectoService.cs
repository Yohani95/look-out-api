using look.domain.entities.proyecto;

namespace look.Application.interfaces.admin
{
    public interface IProyectoService: IService<Proyecto>
    {
        Task<List<Proyecto>> ListComplete();
    }
}

