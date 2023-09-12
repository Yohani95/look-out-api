using look.domain.entities.admin;
using look.domain.interfaces.admin;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;

namespace look.Infrastructure.repository.admin
{
    public class EmailRepository: Repository<Email>, IEmailRepository
    {
        public EmailRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Email>> GetComplete()
        {
            return await _dbContext.Email.Include(e=>e.Cli).Include(e=>e.Per).Include(e=>e.Tem).ToListAsync();
        }
    }
}