using look.domain.entities.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.proyectoDesarrollo
{
    public interface IHitoProyectoDesarrolloRepository : IRepository<HitoProyectoDesarrollo>
    {
        Task<List<HitoProyectoDesarrollo>> GetAllByIdProyecto(int id);
    }
}
