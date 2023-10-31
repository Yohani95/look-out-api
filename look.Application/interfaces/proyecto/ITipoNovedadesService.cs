using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface ITipoNovedadesService: IService<TipoNovedades>
    {
        Task<List<TipoNovedades>> ListComplete();
    }
}

