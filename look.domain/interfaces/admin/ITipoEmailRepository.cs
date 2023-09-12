using look.domain.entities.admin;

namespace look.domain.interfaces.admin
{
    public interface ITipoEmailRepository: IRepository<TipoEmail>
    {
        Task<List<TipoEmail>> GetComplete();
    }
}

