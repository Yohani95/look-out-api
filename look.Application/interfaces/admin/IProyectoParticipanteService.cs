using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;

namespace look.Application.interfaces.admin;

public interface IProyectoParticipanteService: IService<ProyectoParticipante>
{
    Task<List<ProyectoParticipante>> ListComplete();
    /// <summary>
    /// Crea persona como profesional y darle participante a un proyecto 
    /// </summary>
    /// <param name="profesionalesDTO">el dto de profesionales(personas,Participantes)</param>
    /// <returns>retorna resultado de la transaccion</returns>
    Task<ServiceResult> CreateDTOAsync(ProfesionalesDTO profesionalesDTO);
}