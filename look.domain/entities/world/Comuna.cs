using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.world
{
    public class Comuna
    {
        [Key]
        public int ComId { get; set; }

        public string ComNombre { get; set; }

        public int? PrvId { get; set; }

        //public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
        [JsonIgnore]

        public virtual Provincia? Prv { get; set; }
    }
}
