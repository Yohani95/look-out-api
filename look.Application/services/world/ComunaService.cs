using look.Application.interfaces.world;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.world
{
    public class ComunaService : Service<Comuna>, IComunaService
    {
        public ComunaService(IComunaRepository repository) : base(repository)
        {
        }
    }
}
