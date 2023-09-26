using look.Application.interfaces.admin;
using look.domain.entities.admin;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoParticipanteController:BaseController<ProyectoParticipante>
    {
        private readonly IProyectoParticipanteService _participanteService;

        public ProyectoParticipanteController(IProyectoParticipanteService service) : base(service)
        {
            _participanteService = service;
        }

        protected override int GetEntityId(ProyectoParticipante entity)
        {
            return entity.PpaId;
        }
    }
}


