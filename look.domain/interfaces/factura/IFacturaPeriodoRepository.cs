using look.domain.entities.factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.factura
{
    public interface IFacturaPeriodoRepository:IRepository<FacturaPeriodo>
    {
        /// <summary>
        /// trae la lista completa segun el id del periodo
        /// </summary>
        /// <param name="id">id periodo</param>
        /// <returns>retorna una lista</returns>
        Task<List<FacturaPeriodo>> GetAllByIdPeriodo(int id);
        /// <summary>
        /// trae la lista completa segun el estado que no sea pendiente
        /// </summary>
        /// <param name="id">id del estado</param>
        /// <returns>retorna una lista</returns>
        Task<List<FacturaPeriodo>> GetAllByPreSolicitada();
        /// <summary>
        /// cambia de estado segun el periodo de las factura y el estado
        /// </summary>
        /// <param name="idPeriodo">id del periodo de factura</param>
        /// <param name="estado">estado</param>
        /// <returns>retorna un boolean</returns>
        Task<Boolean> ChangeEstado(int idPeriodo, int estado);
        /// <summary>
        /// trae la lista completa segun el id de la horas utilizadas
        /// </summary>
        /// <param name="id">id horas utilizadas</param>
        /// <returns>retorna una lista</returns>
        Task<List<FacturaPeriodo>> GetAllByIdHoras(int id);
        Task<Boolean> ChangeEstadoHoras(int idHoras, int estado);
        Task<List<FacturaPeriodo>> GetAllByIdSoporte(int id);
        Task<Boolean> ChangeEstadoSoporte(int idSoporte, int estado);
    }
}
