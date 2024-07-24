using look.domain.entities.licencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.licencia
{
    public interface ITarifarioVentaLicenciaService:IService<TarifarioVentaLicencia>
    {
        Task<IEnumerable<TarifarioVentaLicencia>> GetAllEntitiesByIdLicense(int idLicencia);
    }
}
