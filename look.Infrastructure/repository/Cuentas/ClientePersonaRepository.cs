using look.domain.entities.cuentas;
using look.domain.interfaces.cuentas;
using look.Infrastructure.data;
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
    }
}
