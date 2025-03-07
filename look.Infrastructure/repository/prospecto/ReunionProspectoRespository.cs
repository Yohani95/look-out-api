﻿using look.domain.entities.prospecto;
using look.domain.interfaces.prospecto;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.prospecto
{
    public class ReunionProspectoRespository : Repository<ReunionProspecto>, IReunionProspectoRepository
    {
        public ReunionProspectoRespository(LookDbContext dbContext) : base(dbContext)
        {
        }

        public async new Task<IEnumerable<ReunionProspecto>> GetAllAsync()
        {
            return await _dbContext.ReunionProspectos
                .Include(r=>r.EstadoReunionProspecto)
                .ToListAsync();
        }
    }
}
