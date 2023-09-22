using look.domain.entities.cuentas;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.proyecto
{
    public class Prospecto
    {
        public int PrsId { get; set; }
        public int? CliId { get; set; }      
        public string? PrsDescripcion { get; set; }
        public DateTime? PrsFechaInicio { get; set; }
        public int? EpsId { get; set; }
        public int? TseId { get; set; }
        public double? PrsPresupuesto { get; set; }
        public int? MonId { get; set; }

        [JsonIgnore]
        public virtual Cliente? Cli { get; set; }

        [JsonIgnore]
        public virtual EstadoProspecto? Esps { get; set; }

        [JsonIgnore]
        public virtual Moneda? Mon { get; set; }

        //public virtual TipoServicio? Tipser { get; set; }



    }
}
