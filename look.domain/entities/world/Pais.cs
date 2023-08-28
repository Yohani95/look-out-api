using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class Pais
    {
        public int PaiId { get; set; }
        public string PaiNombre { get; set; }
        public int LenId { get; set; }

        [JsonIgnore]
        public Lenguaje? Lenguaje { get; set; }

    }
}
