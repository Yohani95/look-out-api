﻿using look.domain.entities.prospecto;
using look.domain.interfaces.prospecto;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.prospecto
{
    public class EstadoReunionProspectoRepository : Repository<EstadoReunionProspecto>, IEstadoReunionProspectoRepository
    {
        public EstadoReunionProspectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }
    }
}
