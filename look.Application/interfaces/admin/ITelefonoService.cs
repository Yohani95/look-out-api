using look.domain.entities.admin;

namespace look.Application.interfaces.admin
{
    public interface ITelefonoService: IService<Telefono>
    {
        Task<List<Telefono>> ListComplete();
    }

}

