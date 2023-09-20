using look.domain.dto.cuentas;
using look.domain.entities.admin;
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
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> CreateWithEntities(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cliente>> GetAllWithEntities()
        {
            return await _dbContext.Cliente
                       .Include(c => c.EstadoCliente)
                        .Include(c => c.Giro)
                        .Include(c => c.Pais)
                        .ThenInclude(p => p.Lenguaje)
                        .OrderBy(c => c.CliId)
                        .Include(c => c.SectorComercial).ToListAsync();
        }
        public async Task<List<CuentaDTO>> GetAllClientDTO()
        {
            var query = from c in _dbContext.Cliente.Include(c => c.SectorComercial).Include(c => c.Pais)
                        join e in _dbContext.Email.Where(email => email.EmaPrincipal == 1)
                        on c.CliId equals e.CliId into emailGroup
                        from email in emailGroup.DefaultIfEmpty()
                        select new CuentaDTO
                        {
                            Cliente = c,
                            email = email.EmaEmail,
                        };

            var result = await query.OrderBy(dto => dto.Cliente.CliId).ToListAsync();
            return result;
        }
    }
}
