using look.domain.entities.Common;
using look.domain.entities.world;

namespace look.Application.interfaces.admin
{
    public interface IDireccionService: IService<Direccion>
    {
        Task<List<Direccion>> ListComplete();
        
        Task<ServiceResult> Create(Direccion email);
        Task<ServiceResult> Edit(Direccion email, int id);
        /// <summary>
        /// obtiene la direcciones segun el id de persona
        /// </summary>
        /// <param name="id">id de persona</param>
        /// <returns>retorna una lista de direcciones</returns>
        Task<List<Direccion>> GetbyIdPersona(int id);
    }
}

