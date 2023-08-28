using look.domain.entities.world;
using look.domain.interfaces.world;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.World
{
    public class PaisRepository : Repository<Pais>, IPaisRepository
    {
        public PaisRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
