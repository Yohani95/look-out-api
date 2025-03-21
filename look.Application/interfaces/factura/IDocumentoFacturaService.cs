﻿using look.domain.entities.factura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.factura
{
    public interface IDocumentoFacturaService : IService<DocumentosFactura>
    {
        Task<DocumentosFactura> AddDocumento(DocumentosFactura entity, DateTime fecha, int idFacturaPeriodo);

        Task<DocumentosFactura> AddDocumentoAnulado(DocumentosFactura entity, int idFacturaPeriodo);
    }
}
