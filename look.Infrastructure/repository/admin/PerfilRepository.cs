using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.admin
{
    public class PerfilRepository : Repository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
