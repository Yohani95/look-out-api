using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;

namespace look.Application.services.admin
{
    public class TelefonoService: Service<Telefono>, ITelefonoService
    {
        private readonly ITelefonoRepository _telefonoRepository;
        
        public TelefonoService(ITelefonoRepository repository) : base(repository)
        {
            _telefonoRepository = repository;
        }

        public async Task<List<Telefono>> ListComplete()
        {
            return await _telefonoRepository.GetComplete();
        }
    }
}

