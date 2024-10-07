using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.proyectoDesarrollo
{
    public class DocumentoProyectoDesarrollo
    {
        public int Id { get; set; }
        public int IdProyectoDesarrollo { get; set; }
        public string? NombreDocumento { get; set; }
        public byte[]? ContenidoDocumento { get; set; }
        public string? Descripcion { get; set; }
    }
}
