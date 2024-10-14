using look.domain.entities.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.proyectoDesarrollo
{
    public interface IHitoProyectoDesarrolloService : IService<HitoProyectoDesarrollo>
    {
        Task<List<HitoProyectoDesarrollo>> GetAllByIdProyecto(int id);
    }
}
