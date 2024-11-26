using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyectoDesarrollo
{
    public class DocumentoProyectoDesarrolloRepository : Repository<DocumentoProyectoDesarrollo>, IDocumentoProyectoDesarrolloRepository
    {
        public DocumentoProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<DocumentoProyectoDesarrollo>> GetByidProyectoDesarrollo(int id)
        {
            return await _dbContext.DocumentoProyectoDesarrollos.Where(d => d.IdProyectoDesarrollo == id).ToListAsync();
        }
    }
}
