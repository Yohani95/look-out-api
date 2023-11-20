using look.domain.entities.proyecto;
using System.ComponentModel.DataAnnotations;

namespace look.domain.entities.admin
{
    public class ProyectoParticipante
    {
        [Key]
        public int PpaId { get; set; }

        public int? PryId { get; set; }

        public int? PerId { get; set; }
        
        public int? CarId { get; set; }
        
        public int? PerTarifa { get; set; }
        
        public int PrfId {get; set; }

        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaTermino { get; set; }
        public int? estado { get; set; }

        public virtual Car? Car { get; set; }
        
        public virtual Persona? Persona { get; set; }
        
        public virtual Proyecto? Proyecto { get; set; }
        
        public virtual Perfil? Perfil { get; set; }
    }
}

