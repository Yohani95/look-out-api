using look.domain.entities.proyecto;

namespace look.domain.interfaces.proyecto
{
    public interface ITarifarioConvenioRepository:IRepository<TarifarioConvenio>
    {
        Task<List<TarifarioConvenio>> GetComplete();
        Task<TarifarioConvenio> GetbyIdEntities(int id);

    }
}

