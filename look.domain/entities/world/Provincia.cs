using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class Provincia
    {
        [Key]
        public int PrvId { get; set; }

        public string? PrvNombre { get; set; }

        public int? RegId { get; set; }

        public virtual ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();
        [JsonIgnore]
        public virtual Region? Reg { get; set; }
    }
}
