using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.proyecto
{
    public class NovedadesRepository: Repository<Novedades>, INovedadesRepository
    {
        public NovedadesRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Novedades>> GetByProjectIdPersonId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Novedades>> GetComplete()
        {
            return await _dbContext.Novedades
                .Include(p=>p.Proyecto)
                .Include(p=>p.Persona)
                .Include(p=>p.Perfil)
                .Include(p=>p.TipoNovedades)
                .ToListAsync();
        }
    }
}

