using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace look_out_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        [HttpGet("error")]
        public IActionResult GenerateError()
        {
            // Simulación de un error no controlado
            throw new Exception("This is a test error");

            // En un escenario real, aquí iría el código del controlador
            // que podría generar un error no controlado
        }
        [HttpGet("handled-error")]
        public IActionResult GenerateHandledError()
        {
            try
            {
                // Simulación de un error controlado
                throw new Exception("This is a handled test error");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Handled error occurred");
                // Aquí podrías agregar la lógica necesaria para manejar el error de forma específica

                // Retorna un resultado adecuado (en este caso, un código 500 para indicar un error interno del servidor)
                return StatusCode(500, "An internal server error occurred");
            }
        }
    }
}
