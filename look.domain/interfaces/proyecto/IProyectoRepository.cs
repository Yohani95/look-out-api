using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface IProyectoRepository:IRepository<Proyecto>
    {
        Task<List<Proyecto>> GetComplete();
    }
}


    
