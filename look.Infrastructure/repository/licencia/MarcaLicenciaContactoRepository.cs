using look.domain.entities.licencia;
using look.domain.interfaces.licencia;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.licencia
{
    public class MarcaLicenciaContactoRepository : Repository<MarcaLicenciaContacto>, IMarcaLicenciaContactoRepository
    {
        public MarcaLicenciaContactoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
