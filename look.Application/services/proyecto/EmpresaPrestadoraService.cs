using look.Application.interfaces.proyecto;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.proyecto
{
    public class EmpresaPrestadoraService : Service<EmpresaPrestadora>, IEmpresaPrestadoraService
    {
        public EmpresaPrestadoraService(IEmpresaPrestadoraRepository repository) : base(repository)
        {
        }
    }
}
