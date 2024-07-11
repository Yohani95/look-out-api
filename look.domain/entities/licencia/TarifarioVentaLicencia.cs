using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.licencia
{
    public class TarifarioVentaLicencia
    {
        public int Id { get; set; }
        public int IdMarcaLicencia { get; set; }
        public int IdMayoristaLicencia { get; set; }
        public int IdLicencia { get; set; }
        public double Valor { get; set; }
        public DateTime FechaVigencia { get; set; }
        public DateTime FechaTermino { get; set; }
        
        public virtual MarcaLicencia? MarcaLicencia { get; }
        public virtual MayoristaLicencia? MayoristaLicencia{ get; }

    }
}
