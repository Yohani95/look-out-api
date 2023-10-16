using look.domain.dto.admin;
using look.domain.entities.admin;
using look.domain.entities.Common;

namespace look.Application.interfaces.admin
{
    public interface IPersonaService:IService<Persona>
    {
        Task<List<Persona>> GetAllByType(int typePersonId);
        Task<ServiceResult> Create(PersonaDTO personaDTO);
        Task<ServiceResult> Edit(int id,PersonaDTO personaDTO);
        Task<ServiceResult> Delete(int id);
        Task<ResponseGeneric<List<PersonaDTO>>> GetAllContactEnteties();
        
        Task<ResponseGeneric<List<PersonaDTO>>> GetAllContact();
        
        Task<ResponseGeneric<List<Persona>>> GetAllContactByIdClient(int id);
    }
}
