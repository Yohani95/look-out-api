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
    public class RolFuncionalidadService : Service<RolFuncionalidad>, IRolFuncionalidadService
    {
        public RolFuncionalidadService(IRolFuncionalidadRepository repository) : base(repository)
        {
        }
    }
}
