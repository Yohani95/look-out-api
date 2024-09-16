using look.domain.entities.licencia;
using look.domain.interfaces.licencia;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.licencia
{
    internal class DocumentoLicenciaRepository : Repository<DocumentoLicencia>, IDocumentoLicenciaRepository
    {
        public DocumentoLicenciaRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<DocumentoLicencia>> GetByIdVentaLicencia(int id)
        {
            return await _dbContext.DocumentoLicencia.Where(d => d.IdLicencia == id).ToListAsync();
        }
    }
}
