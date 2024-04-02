using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.soporte
{
    public class DocumentosSoporte
    {
        public int Id { get; set; }
        public int IdSoporte { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }
        public int? idTipoDocumento { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Soporte? Soporte { get; set; }
    }
}
