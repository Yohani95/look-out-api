using look.domain.entities.Common;
using look.domain.entities.world;

namespace look.Application.interfaces.admin
{
    public interface IDireccionService: IService<Direccion>
    {
        Task<List<Direccion>> ListComplete();
        
        Task<ServiceResult> Create(Direccion email);
        Task<ServiceResult> Edit(Direccion email, int id);
    }
}

