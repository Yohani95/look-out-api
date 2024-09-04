using look.domain.entities.prospecto;
using look.domain.interfaces.prospecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.prospecto
{
    public class ContactoProspectoRepository : Repository<ContactoProspecto>, IContactoProspectoRepository
    {
        public ContactoProspectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async new Task<IEnumerable<ContactoProspecto>> GetAllAsync()
        {
            return await _dbContext.ContactoProspectos
                .Include(p => p.TipoContactoProspecto)
                .Include(p => p.Pais)
                .Include(p => p.Perfil)
                .ToListAsync();
        }
    }
}
