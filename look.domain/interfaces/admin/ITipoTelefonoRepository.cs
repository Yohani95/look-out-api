using look.domain.entities.admin;

namespace look.domain.interfaces.admin
{
    public interface ITipoTelefonoRepository: IRepository<TipoTelefono>
    {
        Task<List<TipoTelefono>> GetComplete();
    }
}

