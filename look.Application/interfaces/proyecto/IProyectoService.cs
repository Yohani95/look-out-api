using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.proyecto
{
    public interface IProyectoService : IService<Proyecto>
    {
        /// <summary>
        /// Obtiene el último ID registrado de la tabla proyecto
        /// </summary>
        /// <returns>Retorna un entero</returns>
        Task<ResponseGeneric<int>> GetLastId();
        /// <summary>
        /// Crea un proyecto con sus entidades y archivos
        /// </summary>
        /// <param name="proyecto">entidad proyecto</param>
        /// <returns></returns>
        Task<ServiceResult> createAsync(IFormFile file1,IFormFile file2,Proyecto proyecto);
    }
}
