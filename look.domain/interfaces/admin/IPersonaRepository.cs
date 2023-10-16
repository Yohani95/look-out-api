using look.domain.dto.admin;
using look.domain.entities.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.admin
{
    public interface IPersonaRepository:IRepository<Persona>
    {
        Task<List<Persona>> GetAllByType(int typePersonId);
        Task<List<PersonaDTO>> GetAllContactEnteties();
        
        Task<List<PersonaDTO>> GetAllContact();

    }
}
