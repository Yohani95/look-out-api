using Microsoft.AspNetCore.Http;
using Microsoft.SharePoint.Client;
using Microsoft.Identity.Client;
using look.domain.interfaces.Common;
using look.domain.entities.Common;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace look.Application.services.Common
{
    public class FileServices
    {
        private readonly ISharePointConfig _sharePointConfig;

        private readonly string _serverUploadPath = AppDomain.CurrentDomain.BaseDirectory;

        private readonly ILogger _logger = (ILogger)domain.entities.Common.Logger.GetLogger();
        public FileServices(IOptions<SharePointConfig> sharePointConfig)
        {
            _sharePointConfig = sharePointConfig.Value;
        }
        // Método para subir un archivo
        public static  async Task<string> UploadFileAsync(IFormFile file, int clientId)
        {


            // Combina la ruta de la carpeta de carga en el servidor con la carpeta raíz de tu proyecto
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo no es válido.");

            // Genera un nombre único para el archivo
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Construye la ruta de la carpeta del cliente y el archivo
            var clientFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "File", clientId.ToString());
            var filePath = Path.Combine(clientFolderPath, fileName);

            // Asegúrate de que la carpeta del cliente exista
            if (!Directory.Exists(clientFolderPath))
                Directory.CreateDirectory(clientFolderPath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath; // Devuelve la ruta del archivo guardado en el servidor
        }

        /// <summary>
        /// Método para eliminar un archivo
        /// </summary>
        /// <param name="filePath"></param>
        public static bool DeleteFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true; 
                }

                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el archivo: {ex.Message}");
                return false; 
            }
        }

        /// <summary>
        /// Método para obtener el formato de un archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetFileFormat(IFormFile file)
        {
            if (file == null)
                throw new ArgumentException("El archivo no es válido.");

            return Path.GetExtension(file.FileName).ToLowerInvariant();
        }
        
        public static FileStream GetFile(string filePath)
        {
            try
            {
                if (filePath == null)
                {
                    throw new ArgumentException();
                }
                if (System.IO.File.Exists(filePath))
                {
                    // Devuelve el archivo como una descarga.
                    var archivoStream = System.IO.File.OpenRead(filePath);
                    return archivoStream;
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
        /// <summary>
        /// Método para subir un archivo a SharePoint
        /// </summary>
        /// <param name="file"></param>
        /// <param name="azureSharePointUrl"></param>
        /// <param name="clientId"></param>
        /// <param name="authority"></param>
        /// <param name="libraryName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> UploadFileToAzureSharePointAsync(IFormFile file, string azureSharePointUrl, string clientId, string authority, string libraryName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo no es válido.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var app = PublicClientApplicationBuilder.Create(clientId)
                .WithAuthority(authority)
                .Build();

            var scopes = new[] { "https://yourtenant.sharepoint.com/.default" }; // Reemplaza con el ámbito adecuado

            var authResult = await app.AcquireTokenInteractive(scopes)
                .WithPrompt(Prompt.SelectAccount)
                .WithUseEmbeddedWebView(false)
                .ExecuteAsync();

            using (var context = new ClientContext(azureSharePointUrl))
            {
                context.ExecutingWebRequest += (sender, e) =>
                {
                    e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + authResult.AccessToken;
                };

                var library = context.Web.Lists.GetByTitle(libraryName);
                var fileInfo = new FileCreationInformation
                {
                    ContentStream = file.OpenReadStream(),
                    Url = fileName,
                    Overwrite = true
                };

                var uploadedFile = library.RootFolder.Files.Add(fileInfo);
                context.ExecuteQuery();

                return uploadedFile.ServerRelativeUrl;
            }
        }
        /// <summary>
        /// Método para eliminar un archivo a SharePoint
        /// </summary>
        /// <param name="azureSharePointUrl"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="libraryName"></param>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFileFromAzureSharePointAsync(string azureSharePointUrl, string clientId, string clientSecret, string libraryName, string relativeFilePath)
        {
            try
            {
                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(new Uri("https://login.microsoftonline.com/your-tenant-id"))
                    .Build();

                var authResult = await confidentialClientApplication.AcquireTokenForClient(new[] { "https://your-tenant.sharepoint.com/.default" }).ExecuteAsync();

                using (var context = new ClientContext(azureSharePointUrl))
                {
                    context.ExecutingWebRequest += (sender, e) =>
                    {
                        e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + authResult.AccessToken;
                    };

                    var library = context.Web.Lists.GetByTitle(libraryName);
                    var fileToDelete = library.RootFolder.Files.GetByUrl(relativeFilePath);

                    fileToDelete.DeleteObject();
                    context.ExecuteQuery();

                    // Si llegamos a este punto, la eliminación fue exitosa
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción en caso de error
                Console.WriteLine($"Error al eliminar el archivo: {ex.Message}");
                return false; // Indicar que la eliminación falló
            }
        }
    }
}
