﻿using look.domain.entities.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.proyectoDesarrollo
{
    public interface IDocumentoProyectoDesarrolloService : IService<DocumentoProyectoDesarrollo>
    {
        Task<List<DocumentoProyectoDesarrollo>> GetByidProyectoDesarrollo(int id);
    }
}
