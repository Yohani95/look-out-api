using look.domain.entities.oportunidad;
using look.domain.entities.proyectoDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.proyectoDesarrollo
{
    public interface IDocumentoProyectoDesarrolloRepository : IRepository<DocumentoProyectoDesarrollo>
    {
        Task<List<DocumentoProyectoDesarrollo>> GetByidProyectoDesarrollo(int id);
    }
}
