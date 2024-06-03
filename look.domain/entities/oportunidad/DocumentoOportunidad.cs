
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace look.domain.entities.oportunidad
{
    public class DocumentoOportunidad
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }
        
        [NotMapped]
        [JsonIgnore]
        public virtual Oportunidad? Oportunidad{ get; set; }
    }
}
