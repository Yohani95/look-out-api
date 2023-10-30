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
   /// <summary>
   /// elimina segun el rut
   /// </summary>
   /// <param name="rut">rut o dni de la persona</param>
   /// <returns></returns>
    Task<ServiceResult> deletedAsync(string rut);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ResponseGeneric<List<ProyectoParticipante>>> GetByIdProyecto(int id);

}