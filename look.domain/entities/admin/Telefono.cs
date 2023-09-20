using look.domain.entities.cuentas;

namespace look.domain.entities.admin
{
    
    public class Telefono
    {
        public int telId { get; set; }
        public int? cliId { get; set; }
        public int? perId { get; set; }
        public String? telNumero { get; set; }
        public int? tteId { get; set; }
        public int? telVigente { get; set; }
        public int? TelPrincipal { get; set; }
        
        public TipoTelefono? tipoTelefono { get; set;}
        public Cliente? cliente { get; set;}
        public Persona? persona { get; set;}
    }
}
