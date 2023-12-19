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
    public int? TcTarifa { get; set; }
    public int TcMoneda { get; set; }
    public int? TcBase { get; set; }
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
}