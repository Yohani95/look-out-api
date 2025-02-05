using look.domain.entities.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.proyectoDesarrollo
{
    public interface IRegistroHorasProyectoDesarrolloRepository : IRepository<RegistroHorasProyectoDesarrollo>
    {
        Task<List<RegistroHorasProyectoDesarrollo>> GetByIdProfesional(int idProfesional);
    }
}
