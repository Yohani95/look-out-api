using look.Application.interfaces.admin;
using look.domain.entities.world;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDireccionController :BaseController<TipoDireccion>
    {
        private readonly ITipoDireccionService _tipoDireccionService;

        public TipoDireccionController(ITipoDireccionService service) : base(service)
        {
            _tipoDireccionService = service;
        }
        
        protected override int GetEntityId(TipoDireccion entity)
        {
            return entity.TdiId;
        }
        
    }
}

