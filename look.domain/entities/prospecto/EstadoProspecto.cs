using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class EstadoProspecto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(200)]
        public string? Descripcion { get; set; }
        [JsonIgnore]
        public virtual ICollection<Prospecto>? Prospectos { get; set; }
    }
}
