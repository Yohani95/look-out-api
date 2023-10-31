using look.domain.entities.admin;

namespace look.domain.interfaces.admin
{
    public interface ITelefonoRepository:IRepository<Telefono>
    {
        Task<List<Telefono>> GetComplete();
        
        Task<List<Telefono>> ListCompleteByIdPersona(int id);
    }
}