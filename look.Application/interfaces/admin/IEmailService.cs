using look.domain.entities.admin;
using look.domain.entities.Common;

namespace look.Application.interfaces.admin
{
    public interface IEmailService: IService<Email>
    {
        Task<List<Email>> ListComplete();
        /// <summary>
        /// obtiene email segun el id de personas
        /// </summary>
        /// <param name="id">id de persona</param>
        /// <returns>retorna una lista de email</returns>
        Task<List<Email>> ListCompleteById(int id);
        Task<ServiceResult> Create(Email email);
        Task<ServiceResult> Edit(Email email, int id);
    }
}