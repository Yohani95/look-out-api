﻿using look.domain.entities.proyecto;
using look.domain.interfaces.proyecto;
using look.Infrastructure.data;
using MyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.repository.proyecto
{
    public class EstadoProyectoRepository : Repository<EstadoProyecto>, IEstadoProyectoRepository
    {
        public EstadoProyectoRepository(LookDbContext dbContext) : base(dbContext)
        {
        }

    }
}
