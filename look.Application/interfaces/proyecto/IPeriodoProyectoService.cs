using look.domain.dto.proyecto;
using look.domain.entities.Common;
using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface IPeriodoProyectoService: IService<PeriodoProyecto>
    {
        Task<List<PeriodoProyecto>> ListComplete();
        Task<List<PeriodoProyecto>> ListByProyecto(int id);
        /// <summary>
        /// se crea el periodo calculando, el  monto segun los participante que este tuvo 
        /// ademas se realiza el calculo en base de sus novedades del participante
        /// </summary>
        /// <param name="periodo">el periodo a crear</param>
        /// <returns>retorna un resultado de servicio</returns>
        Task<ServiceResult> CreateAsync(PeriodoProyecto periodo);
    }
}

