using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.soporte
{
    public class DocumentosSoporteRepository : Repository<DocumentosSoporte>, IDocumentosSoporteRepository
    {
        public DocumentosSoporteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
