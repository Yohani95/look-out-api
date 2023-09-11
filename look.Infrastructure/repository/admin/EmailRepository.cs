﻿using look.domain.entities.admin;
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
            return await _dbContext.Email.Include(u=>u.tipoEmail).Include(u=>u.cliente).Include(u=>u.persona).ToListAsync();
        }
    }
}

