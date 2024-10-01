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

        //constantes como propiedades estáticas
        public static readonly EstadoProspecto Pendiente = new EstadoProspecto { Id = 1, Nombre = "Pendiente", Descripcion = "Pendiente" };
        public static readonly EstadoProspecto EnGestion = new EstadoProspecto { Id = 2, Nombre = "En Gestión", Descripcion = "En Gestión" };
        public static readonly EstadoProspecto Convertido = new EstadoProspecto { Id = 3, Nombre = "Convertido", Descripcion = "Convertido" };
        public static readonly EstadoProspecto Desechado = new EstadoProspecto { Id = 4, Nombre = "Desechado", Descripcion = "Desechado" };
        public static readonly EstadoProspecto Nurturing = new EstadoProspecto { Id = 5, Nombre = "Nurturing", Descripcion = "Nurturing" };
    }
}
