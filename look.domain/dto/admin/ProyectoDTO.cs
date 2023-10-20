using look.domain.entities.proyecto;

namespace look.domain.dto.admin;

public class ProyectoDTO
{
    public Proyecto? Proyecto { get; set; }
    
    public List<TarifarioConvenio>? TarifarioConvenio { get; set; }
}