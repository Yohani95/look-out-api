using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;

namespace look.Application.services.admin
{
    public class TipoTelefonoService: Service<TipoTelefono>, ITipoTelefonoService
    {
        private readonly ITipoTelefonoRepository _tipoTelefonoRepository;
        
        public TipoTelefonoService(ITipoTelefonoRepository repository) : base(repository)
        {
            _tipoTelefonoRepository = repository;
        }

        public async Task<List<TipoTelefono>> ListComplete()
        {
            return await _tipoTelefonoRepository.GetComplete();
        }
    }
}

