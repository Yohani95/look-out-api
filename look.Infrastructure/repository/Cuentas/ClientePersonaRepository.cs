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
    }
}
