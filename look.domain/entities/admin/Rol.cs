using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.admin
{
    public class Rol
    {
        public int RolId { get; set; }

        public string? RolNombre { get; set; }

        public string? RolDescripcion { get; set; }

        public virtual List<RolFuncionalidad>? Funcionalidades { get; set; }
    }
}
