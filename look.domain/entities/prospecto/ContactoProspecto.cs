using look.domain.entities.admin;
using look.domain.entities.proyecto;
using look.domain.entities.world;
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
        public int? IdPais { get; set; }
        [MaxLength(200)]
        public string? Cargo { get; set; }
        //relaciones 
        [ForeignKey(nameof(IdPais))]
        public virtual Pais? Pais { get; }
        [ForeignKey(nameof(IdTipo))]
        public virtual TipoContactoProspecto? TipoContactoProspecto { get; }

        [JsonIgnore]
        public ICollection<Prospecto>? Prospectos { get; set; }

    }
}
