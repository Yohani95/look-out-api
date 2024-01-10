using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace look_out_api.Controllers
{
    [ApiController]
    [Route("api/appinfo")]
    public class AppInfoController : Controller
    {
        private readonly IConfiguration _configuration;

        public AppInfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAppInfo()
        {
            var lastCommitInfo = GetLastCommitInfo(); // Obtener información del último commit

            var appInfo = new
            {
                Version = _configuration["AppInfo:Version"],
                Status = _configuration["AppInfo:Status"],
                LastUpdated = lastCommitInfo.DateTime.ToString("dd/MM/yyy HH:mm"),
                LastCommitMessage = lastCommitInfo.Message
                // Puedes agregar más propiedades del commit si lo necesitas
            };

            return Ok(appInfo);
        }
        private (DateTime DateTime, string Message) GetLastCommitInfo()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "log -1 --pretty=format:\"%h|%cd|%s\"", // Formato de salida: hash|fecha|mensaje
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var commitInfoParts = output.Split('|');
            if (commitInfoParts.Length >= 3)
            {
                var format = "ddd MMM d HH:mm:ss yyyy K"; // Formato de fecha personalizado para analizar la fecha de git
                if (DateTime.TryParseExact(commitInfoParts[1], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime lastCommitDate))
                {
                    var commitMessage = $"{commitInfoParts[0]}: {commitInfoParts[2].Replace("\n", " ")}"; // Reemplaza los caracteres de nueva línea con espacios
                    return (lastCommitDate, commitMessage);
                }
            }

            return (DateTime.UtcNow, "Commit message not found"); // Si hay algún error, regresa información predeterminada
        }
    }
}
