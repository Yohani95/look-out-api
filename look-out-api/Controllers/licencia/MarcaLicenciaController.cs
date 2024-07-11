using look.Application.interfaces;
using look.Application.interfaces.licencia;
using look.domain.entities.licencia;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.licencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaLicenciaController : BaseController<MarcaLicencia>
    {
        public MarcaLicenciaController(IMarcaLicenciaService service) : base(service)
        {
        }

        protected override int GetEntityId(MarcaLicencia entity)
        {
            return entity.Id;
        }
    }
}
