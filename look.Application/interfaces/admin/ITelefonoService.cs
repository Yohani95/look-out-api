using look.domain.entities.admin;
using look.domain.entities.Common;

namespace look.Application.interfaces.admin
{
    public interface ITelefonoService: IService<Telefono>
    {
        Task<List<Telefono>> ListComplete();
        
        Task<ServiceResult> Create(Telefono email);
        Task<ServiceResult> Edit(Telefono email, int id);
        
    }

}

