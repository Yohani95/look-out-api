using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.cuentas
{
    public class Giro
    {
        public int GirId { get; set; }
        public string GirNombre { get; set; }
        public string GirDescripcion { get; set; }
        public int? GirVigente { get; set; }
    }
}
