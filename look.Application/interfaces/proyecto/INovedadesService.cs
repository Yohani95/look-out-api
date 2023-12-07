using look.domain.entities.Common;
using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface INovedadesService: IService<Novedades>
    {
        Task<List<Novedades>> ListComplete();
        
        Task<ServiceResult> updateNovedad(Novedades novedad);
        Task<ServiceResult> createNovedad(Novedades novedad);
    }
}

