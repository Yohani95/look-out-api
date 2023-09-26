using look.domain.entities.proyecto;

namespace look.domain.entities.admin
{
    public class ProyectoParticipante
    {
        public int PpaId { get; set; }

        public int? PryId { get; set; }

        public int? PerId { get; set; }
        
        public int? CarId { get; set; }
        
        public virtual Car? Car { get; set; }
        
        public virtual Persona? Per { get; set; }
        
        public virtual Proyecto? Pro { get; set; }
    }
}

