using look.domain.entities.oportunidad;
using look.domain.interfaces.oportunidad;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.oportunidad
{
    public class TipoLicenciaOportunidadRepository : Repository<TipoLicenciaOportunidad>, ITipoLicenciaOportunidadRepository
    {
        public TipoLicenciaOportunidadRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
