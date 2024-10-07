﻿using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using look.domain.interfaces;
using look.domain.interfaces.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.proyectoDesarrollo
{
    public class TipoProyectoDesarrolloService : Service<TipoProyectoDesarrollo>, ITipoProyectoDesarrolloService
    {
        public TipoProyectoDesarrolloService(ITipoProyectoDesarrolloRepository repository) : base(repository)
        {
        }
    }
}
