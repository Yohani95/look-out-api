using look.domain.entities.admin;
using look.domain.entities.Common;

namespace look.Application.interfaces.admin
{
    public interface ITelefonoService: IService<Telefono>
    {
        Task<List<Telefono>> ListComplete();
        /// <summary>
        /// obtiene los telefonos segun el id de persona
        /// </summary>
        /// <param name="id">id de persona</param>
        /// <returns>retorna una lista de telefonos</returns>
        Task<List<Telefono>> ListCompleteById(int id);
        
        Task<ServiceResult> Create(Telefono email);
        Task<ServiceResult> Edit(Telefono email, int id);
        
    }

}

