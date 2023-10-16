using look.domain.dto.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

        public async Task<List<PersonaDTO>> GetAllContactEnteties()
        {
            var query = from p in _dbContext.Persona
                        join cp in _dbContext.ClientePersona
                        on p.Id equals cp.PerId into clientePersonaGroup
                        from clientePersona in clientePersonaGroup.DefaultIfEmpty()
                        join e in _dbContext.Email.Where(email => email.EmaPrincipal == 1)
                        on p.Id equals e.PerId into emailGroup
                        from email in emailGroup.DefaultIfEmpty()
                        join t in _dbContext.Telefono.Where(tel => tel.TelPrincipal == 1)
                        on p.Id equals t.perId into telGroup
                        from telefono in telGroup.DefaultIfEmpty()
                        join c in _dbContext.Cliente
                        on clientePersona.CliId equals c.CliId into clienteGroup
                        from cliente in clienteGroup.DefaultIfEmpty()
                        where p.TpeId == 3
                        orderby p.Id
                        select new PersonaDTO
                        {
                            Email = email != null ? email.EmaEmail : null,
                            Telefono = telefono != null ? telefono.telNumero : null,
                            Persona = p,
                            Cuenta = cliente != null ? cliente.CliNombre : null
                        };

            return await query.ToListAsync();
        }
        
        public async Task<List<PersonaDTO>> GetAllContact()
        {
            var query = from p in _dbContext.Persona
                
                join e in _dbContext.Email.Where(email => email.EmaPrincipal == 1)
                    on p.Id equals e.PerId into emailGroup
                from email in emailGroup.DefaultIfEmpty()
                join t in _dbContext.Telefono.Where(tel => tel.TelPrincipal == 1)
                    on p.Id equals t.perId into telGroup
                from telefono in telGroup.DefaultIfEmpty()
                where p.TpeId == 3
                orderby p.Id
                select new PersonaDTO
                {
                    Email = email.EmaEmail,
                    Telefono = telefono.telNumero,
                    Persona = p
                };

            return await query.ToListAsync();
        }
    }
}
