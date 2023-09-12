using look.domain.entities.admin;

namespace look.Application.interfaces.admin
{
    public interface ITipoTelefonoService: IService<TipoTelefono>
    {
        Task<List<TipoTelefono>> ListComplete();
    }
}

