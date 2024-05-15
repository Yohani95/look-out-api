using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.soporte
{
    public class SoporteRepository : Repository<Soporte>, ISoporteRepository
    {
        public SoporteRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Soporte>> GetAllEntities()
        {
           return await _dbContext.Soportes
                .Include(p => p.Cliente)
                .Include(p => p.TipoServicio)
                .Include(p => p.Pais)
                .Include(p => p.EmpresaPrestadora)
                .Include(p=>p.DocumentosSoporte)
                .ToListAsync();
        }
        public async Task<Soporte> GetAllEntitiesById(int id)
        {
            return await _dbContext.Soportes
                 .Include(p => p.Cliente)
                 .Include(p => p.TipoServicio)
                 .Include(p => p.Pais)
                 .Include(p => p.EmpresaPrestadora)
                 .Include(p => p.DocumentosSoporte)
                 .FirstOrDefaultAsync(p=>p.PryId==id);
        }

        public async Task<List<Soporte>> GetAllEntitiesByIdTipoSoporte(int idTipoSoporte)
        {
            return await _dbContext.Soportes
                 .Include(p => p.Cliente)
                 .Include(p => p.TipoServicio)
                 .Include(p => p.Pais)
                 .Include(p => p.EmpresaPrestadora)
                 .Include(p => p.DocumentosSoporte)
                 .Where(s=> s.IdTipoSoporte==idTipoSoporte)
                 .ToListAsync();
        }
    }
}
