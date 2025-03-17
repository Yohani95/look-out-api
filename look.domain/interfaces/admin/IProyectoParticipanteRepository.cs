using look.domain.entities.admin;
using look.domain.entities.proyecto;

namespace look.domain.interfaces.admin
{
    public interface IProyectoParticipanteRepository:IRepository<ProyectoParticipante>
    {
        Task<List<ProyectoParticipante>> GetComplete();
        Task<List<ProyectoParticipante>> GetParticipanteByIdProAndDate(PeriodoProyecto periodo);

        Task<List<ProyectoParticipante>> GetAllEntitiesByIdsProject(List<int> ids);
        Task<List<ProyectoParticipante>> GetAllEntitiesByDate(DateTime inicio, DateTime termino);

    }
}


    
