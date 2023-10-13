namespace look.domain.entities.proyecto;

public class TarifarioConvenio
{
    public int TcId { get; set; }
    public int? TcPerfilAsignado { get; set; }
    public int? TcTarifa { get; set; }
    public int? TcMoneda { get; set; }
    public int? TcBase { get; set; }
    public int? TcStatus { get; set; }
    public DateTime? TcInicioVigencia { get; set; }
    public DateTime? TcTerminoVigencia { get; set; }
    public string? ComentariosGrales { get; set; }
    public int? PRpId { get; set; }
}