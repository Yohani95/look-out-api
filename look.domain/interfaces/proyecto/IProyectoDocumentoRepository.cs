using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface IProyectoDocumentoRepository:IRepository<ProyectoDocumento>
    {
        Task<List<ProyectoDocumento>> GetComplete();
    }
}

