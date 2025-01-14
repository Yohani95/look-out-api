using look.Application.interfaces;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces;
using look.domain.interfaces.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.admin
{
    public class FuncionalidadService : Service<Funcionalidad>, IFuncionalidadService
    {
        public FuncionalidadService(IFuncionalidadRepository repository) : base(repository)
        {
        }
    }
}
