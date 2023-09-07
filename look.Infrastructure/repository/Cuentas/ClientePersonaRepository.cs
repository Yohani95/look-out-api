using look.domain.dto.admin;
using look.domain.entities.cuentas;
using look.domain.interfaces.cuentas;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.Cuentas
{
    internal class ClientePersonaRepository : Repository<ClientePersona>, IClientePersonaRepository
    {
        public ClientePersonaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteByClient(int id)
        {
            var clientToDelete = await _dbContext.ClientePersona.FirstOrDefaultAsync(p => p.CliId == id);

            if (clientToDelete != null)
            {
                _dbContext.ClientePersona.Remove(clientToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ClientePersona>> FindByClient(int id)
        {
            try
            {
                return await _dbContext.ClientePersona.Where(p => p.CliId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<ClientePersona> FindByClientKam(int id)
        {
            try
            {
                return await _dbContext.ClientePersona.Include(cp => cp.Persona).Where(cp => cp.CliId == id && cp.Persona.TpeId == 2).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<List<ClientePersona>> GetAllClientRelations()
        {
            var query = from c in _dbContext.Cliente.Include(c=>c.Pais).Include(c=>c.SectorComercial)
                        join cp in _dbContext.ClientePersona on c.CliId equals cp.CliId into cpGroup
                        from cpNullable in cpGroup.DefaultIfEmpty()
                        join p in _dbContext.Persona on (cpNullable != null ? cpNullable.PerId : (int?)null) equals p.Id into pGroup
                        from pNullable in pGroup.DefaultIfEmpty()
                        where pNullable == null || pNullable.TpeId == 2
                        select new
                        {
                            Cliente = c,
                            Persona = pNullable
                        };

            var result = await query.ToListAsync();

            // Luego, puedes mapear los resultados a tus objetos ClientePersona como lo desees.
            var clientePersonas = result.Select(r => new ClientePersona
            {
                Cliente = r.Cliente,
                Persona = r.Persona
            }).ToList();

            return clientePersonas;
        }

        public async Task<ClientePersona> GetClientePersonaDTOById(int id)
        {
            var clientePersona = await _dbContext.ClientePersona.Include(cp => cp.Persona).Include(cp => cp.Cliente).Where(cp => cp.Persona.Id == id).FirstOrDefaultAsync();

            if (clientePersona == null)
            {
                // Si no se encuentra un ClientePersona, al menos devuelve una Persona con otros campos nulos.
                return new ClientePersona
                {
                    Persona = await _dbContext.Persona.FirstOrDefaultAsync(p => p.Id == id)
                };
            }

            return clientePersona;
        }

    
    }
}
