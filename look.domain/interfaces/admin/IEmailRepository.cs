using look.domain.entities.admin;

namespace look.domain.interfaces.admin
{
    public interface IEmailRepository:IRepository<Email>
    {
        Task<List<Email>> GetComplete();
        Task<List<Email>> ListCompleteById(int id);
    }
}