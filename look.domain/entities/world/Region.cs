using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class Region
    {
        [Key]
        public int RegId { get; set; }

        public string? RegNombre { get; set; }

        public int? RegNumero { get; set; }
        public int? PaiId { get; set; }
        [JsonIgnore]
        public virtual Pais? Pais { get; set; }

        public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
    }
}
