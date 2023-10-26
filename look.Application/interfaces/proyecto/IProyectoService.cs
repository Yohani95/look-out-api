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
        /// obtiene la lista con sus entidades
        /// </summary>
        /// <returns>retorna una lista de proyecto</returns>
        Task<List<Proyecto>> GetComplete();
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
        Task<ServiceResult> createAsync(List<IFormFile> files, ProyectoDTO proyectoDTO);
        /// <summary>
        /// Actualiza el proyecto y actualiza sus archivos
        /// </summary>
        /// <param name="id">identificador(ID) del proyecto</param>
        /// <returns>retorna un mensaje generico</returns>
        Task<ServiceResult> updateAsync(List<IFormFile> files, ProyectoDTO proyecto);
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
        /// <summary>
        /// obtiene el objeto con sus entidades,segun su id 
        /// </summary>
        /// <param name="id">id de proyecto</param>
        /// <returns>retorna una respuesta generica con la data</returns>
        Task<ResponseGeneric<Proyecto>> GetByIdAllEntities(int id);
    }
}
