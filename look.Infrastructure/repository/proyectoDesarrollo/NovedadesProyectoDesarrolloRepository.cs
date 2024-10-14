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
    public class NovedadesProyectoDesarrolloRepository : Repository<NovedadesProyectoDesarrollo>, INovedadesProyectoDesarrolloRepository
    {
        public NovedadesProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<NovedadesProyectoDesarrollo>> GetAllByIdProyecto(int id)
        {
            return await _dbContext.NovedadesProyectoDesarrollos
                .Include(p => p.TipoNovedad)
                 .Where(n => n.IdProyectoDesarrollo == id)
                 .ToListAsync();
        }
    }
}
