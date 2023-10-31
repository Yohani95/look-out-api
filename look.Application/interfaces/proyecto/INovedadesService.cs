using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface INovedadesService: IService<Novedades>
    {
        Task<List<Novedades>> ListComplete();
    }
}

