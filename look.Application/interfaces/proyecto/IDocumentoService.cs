using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using look.domain.entities.Common;
using Microsoft.AspNetCore.Http;

namespace look.Application.interfaces.proyecto
{
    public interface IDocumentoService : IService<Documento>
    {
        Task<ServiceResult> SendDocuments(List<IFormFile> files,int idProyecto,int idCliente);
    }
}
