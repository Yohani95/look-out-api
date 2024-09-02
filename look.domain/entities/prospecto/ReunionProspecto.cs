using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class ReunionProspecto
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaReunion { get; set; }
        public bool SolicitaPropuesta { get; set; }
        [ForeignKey(nameof(Prospecto))]
        public int IdProspecto { get; set; }
        [MaxLength(255)]
        public string? Detalle { get; set; }
    }
}
