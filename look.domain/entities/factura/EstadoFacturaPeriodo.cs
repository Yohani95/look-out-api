using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.factura
{
    public class EstadoFacturaPeriodo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public static class ConstantesEstadoFactura
        {
            public static int PENDIENTE = 1;
            public static int SOLICITADA = 2;
            public static int FACTURADA = 3;
            public static int PAGADA = 4;
            public static int Enviada= 5;
            public static int Anulada= 6;
        }
    }
}
