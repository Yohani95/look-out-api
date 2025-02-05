using look.domain.entities.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.proyectoDesarrollo
{
    public interface IProfesionalesProyectoDesarrolloRepository : IRepository<ProfesionalesProyectoDesarrollo>
    {
        Task<List<ProfesionalesProyectoDesarrollo>> GetByProyectoDesarrolloId(int id);
    }
}
