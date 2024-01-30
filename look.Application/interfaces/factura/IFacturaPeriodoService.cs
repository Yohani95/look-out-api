using look.domain.entities.factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.factura
{
    public interface IFacturaPeriodoService:IService<FacturaPeriodo>
    {
        /// <summary>
        /// obtiene todas las facturas por periodo
        /// </summary>
        /// <param name="id">id periodo</param>
        /// <returns>retorna una lista</returns>
        Task<List<FacturaPeriodo>> GetAllEntitiesByIdPeriod(int id);
        /// <summary>
        /// trae la lista completa segun el estado que no sea pendiente
        /// </summary>
        /// <param name="id">id del estado</param>
        /// <returns>retorna una lista</returns>
        Task<List<FacturaPeriodo>> GetAllByPreSolicitada();
        Task<Boolean> ChangeEstado(int idPeriodo, int estado);
        Task<FacturaPeriodo> UpdateFactura(FacturaPeriodo entity, int idFacturaPeriodo);

    }
}
