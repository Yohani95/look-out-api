using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class TipoDireccion
    {
        public int TdiId { get; set; }

        public int? TdiNombre { get; set; }

        public sbyte? TdiVigente { get; set; }

        public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
    }
}
