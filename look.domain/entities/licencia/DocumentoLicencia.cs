
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace look.domain.entities.licencia
{
    public class DocumentoLicencia
    {
        public int Id { get; set; }
        public int IdLicencia { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }
        public string? Descripcion { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual VentaLicencia? VentaLicencia { get; set; }
    }
}
