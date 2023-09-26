using look.domain.entities.admin;

namespace look.Application.interfaces.admin;

public interface IProyectoParticipanteService: IService<ProyectoParticipante>
{
    Task<List<ProyectoParticipante>> ListComplete();
}