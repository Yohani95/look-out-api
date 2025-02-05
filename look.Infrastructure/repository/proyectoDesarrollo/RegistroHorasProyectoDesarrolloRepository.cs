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
    public class RegistroHorasProyectoDesarrolloRepository : Repository<RegistroHorasProyectoDesarrollo>, IRegistroHorasProyectoDesarrolloRepository
    {
        public RegistroHorasProyectoDesarrolloRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<RegistroHorasProyectoDesarrollo>> GetByIdProfesional(int idProfesional)
        {
            return await _dbContext.RegistroHorasProyectoDesarrollo
                .Where(r => r.IdProfesionalProyecto == idProfesional)
                .Include(r=>r.ProfesionalProyecto).ThenInclude(p=>p.Persona)
                .ToListAsync();
        }
    }
}
