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
    public class TipoNovedadesProyectoDesarrolloService : Service<TipoNovedadProyectoDesarrollo>, ITipoNovedadesProyectoDesarrolloService
    {
        public TipoNovedadesProyectoDesarrolloService(ITipoNovedadesProyectoDesarrolloRepository repository) : base(repository)
        {
        }
    }
}
