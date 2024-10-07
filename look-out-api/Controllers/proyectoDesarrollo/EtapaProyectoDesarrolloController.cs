using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.Application.services;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtapaProyectoDesarrolloController : BaseController<EtapaProyectoDesarrollo>
    {
        public EtapaProyectoDesarrolloController(IEtapaProyectoDesarrolloService service) : base(service)
        {
        }

        protected override int GetEntityId(EtapaProyectoDesarrollo entity)
        {
            return entity.Id;
        }
    }
}
