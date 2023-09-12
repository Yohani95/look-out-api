using look.domain.entities.admin;

namespace look.Application.interfaces.admin
{
    public interface ITipoEmailService: IService<TipoEmail>
    {
        Task<List<TipoEmail>> ListComplete();
    }
}

