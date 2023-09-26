using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces;
using look.domain.interfaces.admin;

namespace look.Application.services.admin;

public class ProyectoParticipanteService: Service<ProyectoParticipante>, IProyectoParticipanteService
{
    
    private readonly IProyectoParticipanteRepository _proyectoParticipanteRepository;
    
    public ProyectoParticipanteService(IProyectoParticipanteRepository repository) : base(repository)
    {
        _proyectoParticipanteRepository = repository;
    }
    
    public async Task<List<ProyectoParticipante>> ListComplete()
    {
        return await _proyectoParticipanteRepository.GetComplete();
    }
}