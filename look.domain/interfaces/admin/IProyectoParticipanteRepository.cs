using look.domain.entities.admin;
using look.domain.entities.proyecto;

namespace look.domain.interfaces.admin
{
    public interface IProyectoParticipanteRepository:IRepository<ProyectoParticipante>
    {
        Task<List<ProyectoParticipante>> GetComplete();
        Task<List<ProyectoParticipante>> GetParticipanteByIdProAndDate(PeriodoProyecto periodo);

        
    }
}


    
