using look.domain.entities.admin;
using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface INovedadesRepository: IRepository<Novedades>
    {
        Task<List<Novedades>> GetComplete();
        Task<List<Novedades>> GetByProjectIdPersonId(ProyectoParticipante proyectoParticipante);
    }
    
}

