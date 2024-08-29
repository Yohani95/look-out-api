using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class ContactoProspecto
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string? NombreCompleto { get; set; }
        [MaxLength(150)]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? Numero { get; set; }
        [MaxLength(200)]
        public string? PerfilLinkedin { get; set; }
        public int? IdTipo { get; set; }
        [ForeignKey(nameof(IdTipo))]
        public virtual TipoContactoProspecto? Tipo { get; }
        [JsonIgnore]
        public ICollection<Prospecto>? Prospectos { get; set; }
    }
}
