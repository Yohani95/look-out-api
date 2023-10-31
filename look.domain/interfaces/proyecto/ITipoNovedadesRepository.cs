using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface ITipoNovedadesRepository: IRepository<TipoNovedades>
    {
        Task<List<TipoNovedades>> GetComplete();
    }
}

