using look.domain.entities.admin;

namespace look.domain.entities.prospecto
{
    public class Prospecto
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActividad { get; set; }
        public int? IdKam { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdContactoProspecto { get; set; }
        public bool? Contactado { get; set; }
        public int? CantidadLlamadas { get; set; }
        public bool? Responde { get; set; }
        public int? IdEstadoProspecto { get; set; }
        public virtual Persona? Kam { get; set; }
        public virtual Empresa? Empresa { get; set; }
        public virtual ContactoProspecto? ContactoProspecto { get; set; }
        public virtual EstadoProspecto? EstadoProspecto { get; set; }
    }

}