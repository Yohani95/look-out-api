using look.Application.interfaces.world;
using look.Application.services.admin;
using look.domain.entities.world;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.world
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : BaseController<Moneda>
    {
        //se instancia para acceder algun servicio no generico y se asigna en el constructor
        private readonly IMonedaService _monedaService;
        public MonedaController(IMonedaService monedaService) : base(monedaService)
        {
            _monedaService = monedaService;
        }
        
        [HttpGet("getTipoMoneda")]
        public async Task<IActionResult> getTipoMoneda(int idTo,int idFrom,string amount)
        {
            Log.Information("Solicitud getTipoMoneda");
            String result = await _monedaService.consultaMonedaConvertida(idFrom,idTo,amount);
            return Ok(result);
        }
        

        protected override int GetEntityId(Moneda entity)
        {
            // Implementa la lógica para obtener el ID de la entidad Moneda
            return entity.MonId;
        }
    }
}
