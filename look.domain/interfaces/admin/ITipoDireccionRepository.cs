using look.domain.entities.world;

namespace look.domain.interfaces.admin
{
    public interface ITipoDireccionRepository: IRepository<TipoDireccion>
    {
        Task<List<TipoDireccion>> GetComplete();
    }

}

