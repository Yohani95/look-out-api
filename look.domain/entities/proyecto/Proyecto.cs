using look.domain.entities.cuentas;
using look.domain.entities.world;

namespace look.domain.entities.proyecto
{
    public class Proyecto
    {
        public int PryId { get; set; }

        public string PryNombre { get; set; }

        public int PrpId { get; set; }
        
        public int EpyId { get; set; }
        
        public int TseId { get; set; }
        
        public DateTime PryFechaInicioEstimada { get; set; }
        
        public Decimal PryValor { get; set; }
        
        public int MonId { get; set; }
        
        public int PryIdCliente { get; set; }
        
        public DateTime PryFechaCierreEstimada { get; set; }
        
        public DateTime PryFechaCierre { get; set; }
        
        public int PryIdContacto { get; set; }
        
        public int PryIdContactoClave { get; set; }
        
        
        public virtual Cliente? Cliente { get; set; }
        
        public virtual EstadoProyecto? EstadoProyecto { get; set; }
        
        public virtual Moneda? Moneda { get; set; }
        
        public virtual Propuesta? Propuesta { get; set; }
        
        public virtual TipoServicio? TpoServicio { get; set; }
    }
}

