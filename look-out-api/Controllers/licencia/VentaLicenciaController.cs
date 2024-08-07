﻿using look.Application.interfaces;
using look.Application.interfaces.licencia;
using look.domain.entities.licencia;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.licencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaLicenciaController : BaseController<VentaLicencia>
    {
        public VentaLicenciaController(IVentaLicenciaService service) : base(service)
        {
        }

        protected override int GetEntityId(VentaLicencia entity)
        {
            return entity.Id;
        }
    }
}
