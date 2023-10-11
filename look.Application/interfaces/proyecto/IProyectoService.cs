using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using look.domain.dto.admin;

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
        /// <returns>retorna un mensaje generico</returns>
        Task<ServiceResult> createAsync(IFormFile file1,IFormFile file2,ProyectoDTO proyecto);
        /// <summary>
        /// Actualiza el proyecto y actualiza sus archivos
        /// </summary>
        /// <param name="id">identificador(ID) del proyecto</param>
        /// <returns>retorna un mensaje generico</returns>
        Task<ServiceResult> updateAsync(IFormFile file1, IFormFile file2, Proyecto proyecto);
        /// <summary>
        /// Borra un proyecto con sus entidades y los archivos relacionados
        /// </summary>
        /// <param name="id">identificador(ID) del proyecto</param>
        /// <returns>retorna un mensaje generico</returns>
        Task<ServiceResult> deleteAsync(int id);
        /// <summary>
        /// Descarga el archivo del proyecto según su ruta 
        /// </summary>
        /// <param name="path">ruta del archivo</param>
        /// <returns>retorna un archivo</returns>
        Task<List<FileStream>> GetFile(int id);
    }
}
