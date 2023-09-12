using look.domain.entities.admin;
using look.domain.entities.Common;

namespace look.Application.interfaces.admin
{
    public interface IEmailService: IService<Email>
    {
        Task<List<Email>> ListComplete();
        Task<ServiceResult> Create(Email email);
        Task<ServiceResult> Edit(Email email);
    }
}