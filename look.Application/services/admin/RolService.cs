using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using look.domain.interfaces.admin;

namespace look.Application.services.admin
{
    public class RolService : Service<Rol>, IRolService
    {

        public RolService(IRolRepository repository) : base(repository)
        {
        }
    }
}
