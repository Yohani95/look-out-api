using look.Application.interfaces.proyectoDesarrollo;
using look.domain.interfaces;
using look.domain.interfaces.proyectoDesarrollo;
using look.Infrastructure.data.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.proyectoDesarrollo
{
    internal class EtapaPlanificacionProyectoDesarrolloService : Service<EtapaPlanificacionProyectoDesarrollo>, IEtapaPlanificacionProyectoDesarrolloService
    {
        public EtapaPlanificacionProyectoDesarrolloService(IEtapaPlanificacionProyectoDesarrolloRepository repository) : base(repository)
        {
        }
    }
}
