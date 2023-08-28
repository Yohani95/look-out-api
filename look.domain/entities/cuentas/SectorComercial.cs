using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.cuentas
{
    public class SectorComercial
    {
        public int SecId { get; set; }

        public string? SecNombre { get; set; }

        public string? SecDescripcion { get; set; }

        public sbyte? SecVigente { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    }
}
