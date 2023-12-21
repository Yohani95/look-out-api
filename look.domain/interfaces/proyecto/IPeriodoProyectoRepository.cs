using look.domain.entities.admin;
using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface IPeriodoProyectoRepository: IRepository<PeriodoProyecto>
    {
        Task<List<PeriodoProyecto>> GetComplete();
        /// <summary>
        /// obtiene el periodo segun el rango del periodo
        /// </summary>
        /// <param name="periodo"></param>
        /// <returns>retorna un periodo</returns>
        Task<PeriodoProyecto> GetByPeriodoRange(PeriodoProyecto periodo);
    }
}

