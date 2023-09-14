using look.domain.entities.world;

namespace look.domain.interfaces.admin
{
    public interface IDireccionRepository:IRepository<Direccion>
    {
        Task<List<Direccion>> GetComplete();
    }
}

