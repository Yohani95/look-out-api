using System.ComponentModel.DataAnnotations;

namespace look.domain.entities.prospecto
{
    public class Industria
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(200)]
        public string? Detalle { get; set; }

        public ICollection<Empresa>? Empresas { get; set; }
    }

}