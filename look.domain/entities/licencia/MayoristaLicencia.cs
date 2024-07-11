using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.licencia
{
    public class MayoristaLicencia
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono{ get; set; }
        public bool? Estado { get; set; }
        public virtual List<MayoristaLicenciaContacto>? MayoristaLicenciaContactos { get; set; }
    }
}
