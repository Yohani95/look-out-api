using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class ProyectoDocumentoRepository:Repository<ProyectoDocumento>, IProyectoDocumentoRepository
    {
        
        public ProyectoDocumentoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProyectoDocumento>> GetComplete()
        {
            return await _dbContext.ProyectoDocumento.
                Include(e=>e.Proyecto).
                Include(e=>e.Documento).
                Include(e=>e.TipoDocumento).
                ToListAsync();
        }
    
    }
}

