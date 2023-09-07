using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.admin
{
    public class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        public PersonaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Persona>> GetAllByType(int typePersonId)
        {
            return await _dbContext.Persona.Include(p=>p.TipoPersona)
                .Where(p => p.TpeId == typePersonId).ToListAsync();
        }


    }
}
