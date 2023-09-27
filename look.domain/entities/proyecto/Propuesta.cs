using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.proyecto
{
    public class Propuesta
    {
        public int PrpId { get; set; }
        /// <summary>
        /// id prospecto
        /// </summary>
        public int? PrsId { get; set; }
        public string? PrpDescripcion { get; set; }
        public double? PrpPresupuesto { get; set; }
        public int? MonId { get; set; }
        public int? EppId { get; set; }
        public int? TseId { get; set; }

        
        [JsonIgnore]
        public virtual EstadoPropuesta? EsPro { get; set; }

        [JsonIgnore]
        public virtual Moneda? Mon { get; set; }

        [JsonIgnore]
        public virtual Prospecto? Prosp { get; set; }

        [JsonIgnore]
        public virtual TipoServicio? TipSer { get; set; }

    }
}
