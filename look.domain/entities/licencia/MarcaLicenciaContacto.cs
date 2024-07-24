using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.licencia
{
    public class MarcaLicenciaContacto
    {
        public int Id { get; set; }
        public int IdMarca { get; set; }
        public int IdContacto { get; set; }
    }
}
