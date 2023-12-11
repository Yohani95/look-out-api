using look.domain.entities.Common;
using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface INovedadesService: IService<Novedades>
    {
        Task<List<Novedades>> ListComplete();
        /// <summary>
        /// actualiza una novedad y ademas si es necesario actualiza el rol
        /// </summary>
        /// <param name="novedad"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> updateNovedad(Novedades novedad, int id);
        /// <summary>
        /// crea una novedad y ademas si es necesario actualiza el rol
        /// </summary>
        /// <param name="novedad"></param>
        /// <returns></returns>
        Task<ServiceResult> createNovedad(Novedades novedad);
    }
}

