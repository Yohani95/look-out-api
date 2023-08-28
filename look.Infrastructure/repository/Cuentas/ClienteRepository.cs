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

        public async Task<List<Cliente>> GetAllWithEntites()
        {
            return await _dbContext.Cliente
                       .Include(c => c.EstadoCliente)
                        .Include(c => c.Giro)
                        .Include(c => c.Pais)
                        .ThenInclude(p => p.Lenguaje)
                        .Include(c => c.SectorComercial).ToListAsync();
        }
    }
}
