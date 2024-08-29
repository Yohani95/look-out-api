using look.domain.entities.admin;
using look.domain.entities.cuentas;
using System.ComponentModel.DataAnnotations.Schema;

namespace look.domain.entities.prospecto
{
    public class Prospecto
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActividad { get; set; }
        public int? IdKam { get; set; }
        public bool? Contactado { get; set; }
        public int? CantidadLlamadas { get; set; }
        public bool? Responde { get; set; }
        public int? IdEstadoProspecto { get; set; }
        public int? IdCliente { get; set; }
        public int? IdContacto { get; set; }
        public virtual Persona? Kam { get; set; }
        public virtual EstadoProspecto? EstadoProspecto { get; set; }
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey(nameof(IdContacto))]
        public virtual ContactoProspecto? Contacto { get; set; }
    }

}