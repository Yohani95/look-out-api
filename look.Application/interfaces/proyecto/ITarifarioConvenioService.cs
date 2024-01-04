using look.domain.entities.Common;
using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface ITarifarioConvenioService: IService<TarifarioConvenio>
    {
        /// <summary>
        /// obtiene una lista completa de tarifario
        /// </summary>
        /// <returns>retorna una lista de tipo tarifario</returns>
        Task<List<TarifarioConvenio>> ListComplete();
        /// <summary>
        /// obtiene el tarifario con sus entidades segun su id
        /// </summary>
        /// <param name="id">id de tarifario</param>
        /// <returns>retorna un tarifario</returns>
        Task<ResponseGeneric<TarifarioConvenio>> GetByIdEntities(int id);
        Task<ResponseGeneric<List<TarifarioConvenio>>> GetByIdProyectoEntities(int idProyecto);
        /// <summary>
        /// obtiene por id con sus entidades
        /// </summary>
        /// <param name="id">id de tarifario</param>
        /// <returns>retornar un tarifario</returns>
        Task<TarifarioConvenio> GetbyIdEntities(int id);
    }
}

