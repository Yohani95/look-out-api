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
    internal class DocumentoOportunidadRepository : Repository<DocumentoOportunidad>, IDocumentoOportunidadRepository
    {
        public DocumentoOportunidadRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
