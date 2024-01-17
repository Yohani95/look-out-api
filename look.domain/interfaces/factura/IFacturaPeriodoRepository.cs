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
    }
}
