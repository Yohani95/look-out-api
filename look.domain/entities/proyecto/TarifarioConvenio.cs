using look.domain.entities.admin;
using look.domain.entities.world;

namespace look.domain.entities.proyecto;

public class TarifarioConvenio
{
    public int TcId { get; set; }
    /// <summary>
    /// id de Entidad Perfil 
    /// </summary>
    public int? TcPerfilAsignado { get; set; }
    public double? TcTarifa { get; set; }
    public int TcMoneda { get; set; }
    public int? TcBase { get; set; }//  hora(solo aplica dia habil 9 horas normalmente(hora*dias(segunperiodo)*tarifa))  ,
                                    //  semana sigue la logica de 5 dias * tarifa, (periodo /semana )* tarifa, quitar 
                                    //  mes sigue la logica de dias osea mes * tarifa
                                    //. [ facturaciones dia habil o mensual=> 01/07 a 15/07, 16/08 a 01/08(incluyendo novedades)]
    public int? TcStatus { get; set; }
    public DateTime? TcInicioVigencia { get; set; }
    public DateTime? TcTerminoVigencia { get; set; }
    public string? ComentariosGrales { get; set; }
    /// <summary>
    /// id de Entidad Proyecto
    /// </summary>
    public int? PRpId { get; set; }

    public virtual Proyecto? Proyecto { get; set; }
    public virtual Perfil? Perfil { get; set; } 
    public virtual Moneda? Moneda{ get; set; } 
    public static class ConstantesTcBase
    {
        public const int Mes= 1;
        public const int Semana = 2;
        public const int Hora = 3;
        public const int Dia = 4;
    }
}