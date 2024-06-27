using look.domain.entities.oportunidad;
using look.domain.interfaces.oportunidad;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.oportunidad
{
    public class EstadoOportunidadRepository : Repository<EstadoOportunidad>, IEstadoOportunidadRepository
    {
        public EstadoOportunidadRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
