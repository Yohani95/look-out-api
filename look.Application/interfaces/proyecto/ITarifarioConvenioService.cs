using look.domain.entities.proyecto;

namespace look.Application.interfaces.proyecto
{
    public interface ITarifarioConvenioService: IService<TarifarioConvenio>
    {
        Task<List<TarifarioConvenio>> ListComplete();
    }
}

