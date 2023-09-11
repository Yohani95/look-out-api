using look.Application.services;
using look.domain.entities.admin;

namespace look.Application.interfaces.admin
{
    public interface IEmailService: IService<Email>
    {
        Task<List<Email>> ListComplete();
    }
}