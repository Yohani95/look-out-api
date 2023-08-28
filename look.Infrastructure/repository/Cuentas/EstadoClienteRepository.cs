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
    public class EstadoClienteRepository : Repository<EstadoCliente>, IEstadoClienteRepository
    {
        public EstadoClienteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
