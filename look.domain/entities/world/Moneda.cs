using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class Moneda
    {
        public int MonId { get; set; }

        public string? MonNombre { get; set; }
        public int? MonVigente { get; set; }
        public int? PaiId { get; set; }
        public Pais? Pais { get; set; }

    }
}
