using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class Empresa
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(255)]
        public string? Detalle { get; set; }
        public int? IdIndustria { get; set; }

        public Industria? Industria { get; set; }
        public ICollection<Prospecto>? Prospectos { get; set; }
    }

}
