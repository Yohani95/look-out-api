using System.ComponentModel.DataAnnotations;

namespace look.domain.entities.proyecto
{
    public class PeriodoProyecto
    {
        [Key]
        public int id { get; set; }
        public int? PryId { get; set; }
        public DateTime? FechaPeriodoDesde { get; set; }
        public DateTime? FechaPeriodoHasta { get; set; }
        public int? estado { get; set; }
        public double? Monto { get; set; }
        public int? NumeroProfesionales { get; set; }
        public virtual Proyecto? Proyecto { get; set; }
    }
}

