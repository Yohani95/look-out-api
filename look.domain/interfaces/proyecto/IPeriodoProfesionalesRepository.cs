﻿using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.proyecto
{
    public interface IPeriodoProfesionalesRepository : IRepository<PeriodoProfesionales>
    {
        Task<List<PeriodoProfesionales>> GetAllEntitiesByIdPeriod(int idPeriodo);
    }
}
