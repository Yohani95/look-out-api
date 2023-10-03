using look.domain.entities.cuentas;
using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.proyecto
{
    public class Documento
    {
        [Key]
        public int DocId { get; set; }
        public string? DocNombre { get; set; }
        public int? TdoId { get; set; }
        public string? DocUrl { get; set; }
        public string? DocExtencion { get; set; }
        public int? DocIdCliente { get; set; }
        public virtual TipoDocumento? TipoDoc { get; set; }

        [JsonIgnore]
        public virtual Cliente? DocCli { get; set; }
    }
}
