using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.cuentas
{
    public class EstadoCliente
    {
        [Key]
        public int EclId { get; set; }
        public string EclNombre { get; set; }

    }
}
