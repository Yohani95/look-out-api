using look.domain.entities.oportunidad;
using look.domain.entities.world;
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

        public int? IdMoneda { get; set; }
        public int? IdVentaLicencia { get; set; }
        public virtual MarcaLicencia? MarcaLicencia { get; }
        public virtual MayoristaLicencia? MayoristaLicencia { get; }
        public virtual TipoLicenciaOportunidad? TipoLicencia { get; }

        public virtual Moneda? Moneda { get; }

    }
}
