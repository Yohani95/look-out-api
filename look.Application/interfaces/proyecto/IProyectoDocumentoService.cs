using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface IProyectoDocumentoService: IService<ProyectoDocumento>
    {
        Task<List<ProyectoDocumento>> ListComplete();
    }
}

