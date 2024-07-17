using look.Application.interfaces;
using look.Application.interfaces.licencia;
using look.domain.entities.licencia;
using look.domain.interfaces.licencia;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.licencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifarioVentaLicenciaController : BaseController<TarifarioVentaLicencia>
    {
        public TarifarioVentaLicenciaController(ITarifarioVentaLicenciaService service) : base(service)
        {
        }

        protected override int GetEntityId(TarifarioVentaLicencia entity)
        {
            return entity.Id;
        }
    }
}
