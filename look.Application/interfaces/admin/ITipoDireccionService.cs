using look.domain.entities.world;

namespace look.Application.interfaces.admin
{
    public interface ITipoDireccionService: IService<TipoDireccion>
    {
        Task<List<TipoDireccion>> ListComplete();
    }
}

