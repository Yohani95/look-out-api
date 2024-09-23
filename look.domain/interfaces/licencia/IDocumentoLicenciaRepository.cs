using look.domain.entities.licencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.licencia
{
    public interface IDocumentoLicenciaRepository : IRepository<DocumentoLicencia>
    {
        Task<List<DocumentoLicencia>> GetByIdVentaLicencia(int id);
    }
}
