﻿using look.Application.interfaces;
using look.Application.interfaces.proyectoDesarrollo;
using look.domain.entities.proyectoDesarrollo;
using Microsoft.AspNetCore.Mvc;

namespace look_out_api.Controllers.proyectoDesarrollo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalesProyectoDesarrolloController : BaseController<ProfesionalesProyectoDesarrollo>
    {
        private readonly IProfesionalesProyectoDesarrolloService _service;
        public ProfesionalesProyectoDesarrolloController(IProfesionalesProyectoDesarrolloService service) : base(service)
        {
            _service = service;
        }

        protected override int GetEntityId(ProfesionalesProyectoDesarrollo entity)
        {
            return entity.Id;
        }
        [HttpGet("GetByProyectoDesarrolloId/{id}")]
        [ProducesResponseType(typeof(List<HitoProyectoDesarrollo>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<ActionResult<List<ProfesionalesProyectoDesarrollo>>> GetByProyectoDesarrolloId(int id)
        {
            try
            {
                var result = await _service.GetByProyectoDesarrolloId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Error retrieving data",
                    Detail = ex.Message
                });
            }
        }
    }
}
