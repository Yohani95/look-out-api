using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;

namespace look.Application.services.admin
{
    public class TipoEmailService: Service<TipoEmail>, ITipoEmailService
    {
        private readonly ITipoEmailRepository _tipoEmailRepository;
        
        public TipoEmailService(ITipoEmailRepository repository) : base(repository)
        {
            _tipoEmailRepository = repository;
        }

        public async Task<List<TipoEmail>> ListComplete()
        {
            return await _tipoEmailRepository.GetComplete();
        }
    }
}

