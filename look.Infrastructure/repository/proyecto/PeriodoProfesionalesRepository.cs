using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyecto
{
    public class PeriodoProfesionalesRepository : Repository<PeriodoProfesionales>, IPeriodoProfesionalesRepository
    {
        public PeriodoProfesionalesRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PeriodoProfesionales>> GetAllEntitiesByIdPeriod(int idPeriodo)
        {
            return await _dbContext.PeriodoProfesionales
                .Include(p => p.Periodo).ThenInclude(p => p.Proyecto)
                .Include(p => p.Participante).ThenInclude(p => p.Persona)
                .Include(p => p.Participante).ThenInclude(p => p.Perfil)
                .Where(p=>p.IdPeriodo == idPeriodo)
                .ToListAsync();
        }
    }
}
