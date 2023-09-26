using look.domain.entities.admin;

namespace look.domain.interfaces.admin
{
    public interface IProyectoParticipanteRepository:IRepository<ProyectoParticipante>
    {
        Task<List<ProyectoParticipante>> GetComplete();
    }
}


    
