using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface INovedadesRepository: IRepository<Novedades>
    {
        Task<List<Novedades>> GetComplete();
    }
    
}

