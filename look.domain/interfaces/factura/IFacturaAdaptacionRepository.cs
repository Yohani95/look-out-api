using look.domain.entities.factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.factura
{
    public interface IFacturaAdaptacionRepository : IRepository<FacturaAdaptacion>
    {
        Task<FacturaAdaptacion> GetAllEntitiesByIdPeriod(int id);
        Task<FacturaAdaptacion> GetAllByIdHoras(int id);
        Task<FacturaAdaptacion> GetAllByIdSoporte(int id);
        Task<FacturaAdaptacion> GetAllEntitiesByIdLicense(int id);
    }
}
