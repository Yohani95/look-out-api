using look.domain.entities.proyecto;

namespace look.domain.dto.admin;

public class ProyectoDTO
{
    public int PryId { get; set; }
    public string? PryNombre { get; set; }
    public int? PrpId { get; set; }
    public int? EpyId { get; set; }
    public int? TseId { get; set; }
    public DateTime? PryFechaInicioEstimada { get; set; }
    public double? PryValor { get; set; }
    public int? MonId { get; set; }
    public int? PryIdCliente { get; set; }
    public DateTime? PryFechaCierreEstimada { get; set; }
    public DateTime? PryFechaCierre { get; set; }
    public int? PryIdContacto { get; set; }
    public int? PryIdContactoClave { get; set; }
    public List<TarifarioConvenio> TarifarioConvenios { get; set; }
}