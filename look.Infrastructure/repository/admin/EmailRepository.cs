using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class EmailRepository: Repository<Email>, IEmailRepository
    {
        public EmailRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}

