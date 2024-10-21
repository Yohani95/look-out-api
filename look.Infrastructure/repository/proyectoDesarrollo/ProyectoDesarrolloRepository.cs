
using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;


namespace look.Infrastructure.repository.proyectoDesarrollo
{
    public class ProyectoDesarrolloRepository : Repository<ProyectoDesarrollo>, IProyectoDesarrolloRepository
    {
        public ProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<ProyectoDesarrollo>> GetAllAsync()
        {
            return await _dbContext.ProyectoDesarrollos
                            .Include(p => p.Kam)
                            .Include(p => p.Cliente)
                            .Include(p => p.Moneda)
                            .Include(p => p.Estado)
                            .Include(p => p.TipoProyectoDesarrollo)
                            .Include(p => p.Etapa)
                            .Include(p => p.EmpresaPrestadora)
                            .ToListAsync();
        }
    }
}
