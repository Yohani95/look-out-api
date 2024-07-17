using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.licencia
{
    public class MayoristaLicenciaContacto
    {
        public int Id { get; set; }
        public int IdMayorista { get; set; }
        public int IdContacto{ get; set; }
    }
}
