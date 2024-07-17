using look.Application.interfaces.licencia;
using look.domain.entities.licencia;
using look.domain.interfaces;
using look.domain.interfaces.licencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.licencia
{
    public class MarcaLicenciaService : Service<MarcaLicencia>, IMarcaLicenciaService
    {
        public MarcaLicenciaService(IMarcaLicenciaRepository repository) : base(repository)
        {
        }
    }
}
