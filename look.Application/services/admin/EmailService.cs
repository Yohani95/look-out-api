using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;

namespace look.Application.services.admin
{
    public class EmailService: Service<Email>, IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        
        public EmailService(IEmailRepository repository) : base(repository)
        {
            _emailRepository = repository;
        }

        public async Task<List<Email>> ListComplete()
        {
            return await _emailRepository.GetComplete();
        }
    }
}