using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using look.domain.interfaces;
using look.domain.interfaces.oportunidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.oportunidad
{
    public class TipoOportunidadService : Service<TipoOportunidad>, ITipoOportunidadService
    {
        public TipoOportunidadService(ITipoOporturnidadRepository repository) : base(repository)
        {
        }
    }
}
