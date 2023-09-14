using look.Application.interfaces.admin;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.admin;

namespace look.Application.services.admin;

public class TipoDireccionService: Service<TipoDireccion>, ITipoDireccionService
{
    private readonly ITipoDireccionRepository _tipoDireccionRepository;
        
    public TipoDireccionService(ITipoDireccionRepository repository) : base(repository)
    {
        _tipoDireccionRepository = repository;
    }

    public async Task<List<TipoDireccion>> ListComplete()
    {
        return await _tipoDireccionRepository.GetComplete();
    }
}