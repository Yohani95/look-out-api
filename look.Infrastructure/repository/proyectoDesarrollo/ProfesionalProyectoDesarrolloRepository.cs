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
    public class ProfesionalProyectoDesarrolloRepository : Repository<ProfesionalesProyectoDesarrollo>, IProfesionalesProyectoDesarrolloRepository
    {
        public ProfesionalProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
        public new async Task<IEnumerable<ProfesionalesProyectoDesarrollo>> GetAllAsync()
        {
            return await _dbContext.ProfesionalesProyectoDesarrollo
                            .Include(p => p.Persona)
                            .ToListAsync();
        }

        public async Task<List<ProfesionalesProyectoDesarrollo>> GetByProyectoDesarrolloId(int id)
        {
            return await _dbContext.ProfesionalesProyectoDesarrollo
                            .Include(p => p.Persona)
                            .Where(p => p.IdProyectoDesarrollo == id)
                            .ToListAsync();
        }
    }
}
