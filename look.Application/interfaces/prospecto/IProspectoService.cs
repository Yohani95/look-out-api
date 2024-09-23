using look.domain.entities.Common;
using look.domain.entities.prospecto;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.prospecto
{
    public interface IProspectoService : IService<Prospecto>
    {
        public Task<ServiceResult> ProcesarCargaMasiva(IFormFile file);
    }
}
