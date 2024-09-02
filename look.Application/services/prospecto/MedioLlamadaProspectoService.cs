using look.Application.interfaces.prospecto;
using look.domain.entities.prospecto;
using look.domain.interfaces;
using look.domain.interfaces.prospecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.prospecto
{
    internal class MedioLlamadaProspectoService : Service<MedioLLamadaProspecto>, IMedioLlamadaProspectoService
    {
        public MedioLlamadaProspectoService(IMedioLlamadaProspectoRepository repository) : base(repository)
        {
        }
    }
}
