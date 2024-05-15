using look.domain.entities.Common;
using look.domain.entities.soporte;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.soporte
{
    public interface ISoporteService:IService<Soporte>
    {
        Task<ServiceResult> createAsync(List<IFormFile> files, Soporte soporte);
        Task<IEnumerable<Soporte>> GetAllEntities();
        Task<Soporte> GetAllEntitiesById(int id);
        Task<List<Soporte>> GetAllEntitiesByIdTipoSoporte(int idTipoSoporte);
    }
}
