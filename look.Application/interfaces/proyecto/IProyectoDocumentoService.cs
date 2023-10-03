using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface IProyectoDocumentoService : IService<ProyectoDocumento>
    {
        Task<List<ProyectoDocumento>> ListComplete();
        /// <summary>
        /// obtiene segun id del proyecto
        /// </summary>
        /// <param name="id">id del proyecto</param>
        /// <returns>retorna un proyectoDocumento</returns>
        Task<List<ProyectoDocumento>> GetByIdProject (int id);
    }
}

