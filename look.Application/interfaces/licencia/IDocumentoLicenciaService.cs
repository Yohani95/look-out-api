﻿using look.domain.entities.licencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.licencia
{
    public interface IDocumentoLicenciaService : IService<DocumentoLicencia>
    {
        Task<List<DocumentoLicencia>> GetByIdVentaLicencia(int id);
    }
}
