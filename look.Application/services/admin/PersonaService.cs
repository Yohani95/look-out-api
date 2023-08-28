using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces;
using look.domain.interfaces.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.admin
{
    public class PersonaService : Service<Persona>, IPersonaService

    {
        private readonly IPersonaRepository _personaRepository;
        public PersonaService(IPersonaRepository repository) : base(repository)
        {
            _personaRepository = repository;
        }

        public async Task<List<Persona>> GetAllByType(int typePersonId)
        {
            return await _personaRepository.GetAllByType(typePersonId);
        }
    }
}
