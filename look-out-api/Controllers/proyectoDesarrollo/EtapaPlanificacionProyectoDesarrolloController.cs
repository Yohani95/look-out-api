using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.Infrastructure.data.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtapaPlanificacionProyectoDesarrolloController : BaseController<EtapaPlanificacionProyectoDesarrollo>
    {
        public EtapaPlanificacionProyectoDesarrolloController(IEtapaPlanificacionProyectoDesarrolloService service) : base(service)
        {
        }

        protected override int GetEntityId(EtapaPlanificacionProyectoDesarrollo entity)
        {
            return entity.Id;
        }
    }
}
