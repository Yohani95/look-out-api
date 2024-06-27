using look.domain.entities.oportunidad;
using look.domain.interfaces.oportunidad;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<DocumentoOportunidad>> GetByidOportunidad(int id)
        {
           return await _dbContext.DocumentoOportunidades.Where(d => d.IdOportunidad == id).ToListAsync();
        }
    }
}
