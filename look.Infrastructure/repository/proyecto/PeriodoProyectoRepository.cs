using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class PeriodoProyectoRepository: Repository<PeriodoProyectos>, IPeriodoProyectoRepository
    {
        public PeriodoProyectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PeriodoProyectos>> GetComplete()
        {
            return await _dbContext.PeriodoProyectos
                .Include(p=>p.Proyecto)
                .ToListAsync();
        }
    }
}

