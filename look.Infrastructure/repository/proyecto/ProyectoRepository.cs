using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class ProyectoRepository:Repository<Proyecto>, IProyectoRepository
    {
        
        public ProyectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Proyecto>> GetComplete()
        {
            return await _dbContext.Proyecto.
                Include(e=>e.TpoServicio).
                Include(e=>e.Cliente).
                Include(e=>e.Propuesta).
                Include(e=>e.EstadoProyecto).
                Include(e=>e.Moneda).
                ToListAsync();
        }
    }
    
}

