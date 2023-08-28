using look.domain.entities.admin;


namespace look.Application.interfaces.admin
{
    public interface IPersonaService:IService<Persona>
    {
        Task<List<Persona>> GetAllByType(int typePersonId);
    }
}
