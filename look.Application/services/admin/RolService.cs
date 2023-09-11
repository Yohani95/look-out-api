using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.admin
{
    public class RolService : Service<Rol>, IRolService
    {

        public RolService(IRepository<Rol> repository) : base(repository)
        {
        }
    }
}
