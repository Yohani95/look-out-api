using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class LlamadaProspecto
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool? RespondeLlamada { get; set; }
        public int? IdMedioLlamadaProspecto { get; set; }

        [ForeignKey(nameof(Prospecto))]
        public int IdProspecto { get; set; }
        [MaxLength(255)]
        public string? Detalle { get; set; }

        [ForeignKey(nameof(IdMedioLlamadaProspecto))]
        public virtual MedioLLamadaProspecto? MedioLLamadaProspecto { get; set; }
    }
}
