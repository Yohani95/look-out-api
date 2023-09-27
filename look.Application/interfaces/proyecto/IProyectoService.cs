using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
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
    }
}
