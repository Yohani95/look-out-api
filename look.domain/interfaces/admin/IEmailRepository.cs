using look.domain.entities.admin;

namespace look.domain.interfaces.admin
{
    public interface IEmailRepository:IRepository<Email>
    {
        Task<List<Email>> GetComplete();
        /// <summary>
        /// obtiene los email segun la persona 
        /// </summary>
        /// <param name="id">id de persona</param>
        /// <returns>retonar una lista de email </returns>
        Task<List<Email>> ListCompleteByIdPersona(int id);
    }
}