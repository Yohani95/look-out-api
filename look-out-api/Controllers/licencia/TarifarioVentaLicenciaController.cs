using look.Application.interfaces;
using look.Application.interfaces.licencia;
using look.Application.services;
using look.domain.entities.factura;
using look.domain.entities.licencia;
using look.domain.interfaces.licencia;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers.licencia
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifarioVentaLicenciaController : BaseController<TarifarioVentaLicencia>
    {
        private readonly ITarifarioVentaLicenciaService _service;
        public TarifarioVentaLicenciaController(ITarifarioVentaLicenciaService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("getAllTarifarioVentaLicenciaByIdLicencia/{id}")]
        public async Task<ActionResult<TarifarioVentaLicencia>> getAllTarifarioVentaLicenciaById(int id)
        {
            Log.Information("[GetAllEntitiesByIdLicense] Solicitud getall tarifarios licencia licencia idLicencia: " + id);
            var data = await _service.GetAllEntitiesByIdLicense(id);
            return Ok(data);
        }
        protected override int GetEntityId(TarifarioVentaLicencia entity)
        {
            return entity.Id;
        }
    }
}
