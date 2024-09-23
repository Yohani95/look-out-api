using look.Application.interfaces.prospecto;
using look.domain.entities.Common;
using look.domain.entities.prospecto;
using look.domain.interfaces.prospecto;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Serilog;

namespace look.Application.services.prospecto
{
    public class ProspectoService : Service<Prospecto>, IProspectoService
    {
        //instanciar repository si se requiere 
        private readonly IProspectoRepository _prospectoRepository;
        private readonly IContactoProspectoRepository _contactoRepository;
        private readonly IReunionProspectoRepository _reunionProspectoRepository;
        private readonly ILlamadaProspectoRepository _llamadaProspectoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();

        public ProspectoService(IProspectoRepository prospectoRepository, IContactoProspectoRepository contactoProspectoRepository,
            ILlamadaProspectoRepository llamadaProspectoRepository, IReunionProspectoRepository reunionProspectoRepository, IUnitOfWork unitOfWork) : base(prospectoRepository)
        {
            _prospectoRepository = prospectoRepository;
            _contactoRepository = contactoProspectoRepository;
            _reunionProspectoRepository = reunionProspectoRepository;
            _llamadaProspectoRepository = llamadaProspectoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> ProcesarCargaMasiva(IFormFile file)
        {
            var result = new ServiceResult();

            try
            {
                if (file == null || file.Length == 0)
                {
                    result.IsSuccess = false;
                    result.Message = "El archivo proporcionado está vacío o no es válido.";
                    result.MessageCode = ServiceResultMessage.InvalidInput;
                    return result;
                }

                using (var stream = new MemoryStream())
                {
                    await _unitOfWork.BeginTransactionAsync();
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // Leer la primera hoja

                        int rowCount = worksheet.Dimension.Rows; // Número de filas

                        for (int row = 2; row <= rowCount; row++) // Saltar la fila de encabezado
                        {
                            // Leer datos de cada celda
                            // Leer datos de cada celda
                            string empresa = worksheet.Cells[row, 1].Text;
                            string nombreCompleto = worksheet.Cells[row, 3].Text;
                            string clasificacion = worksheet.Cells[row, 4].Text;
                            string cargo = worksheet.Cells[row, 5].Text;
                            string correo = worksheet.Cells[row, 6].Text;
                            string telefono = worksheet.Cells[row, 7].Text;
                            string perfilLinkedin = worksheet.Cells[row, 8].Text;
                            string industria = worksheet.Cells[row, 9].Text;

                            // Convertir fecha de carga
                            DateTime? fechaCarga = DateTime.TryParse(worksheet.Cells[row, 10].Text, out DateTime fechaCargaResult)
                                                    ? fechaCargaResult : (DateTime?)null;

                            string asignacion = worksheet.Cells[row, 11].Text;

                            // Convertir booleano "Contactado"
                            bool contactado = worksheet.Cells[row, 12].Text.Trim().ToLower() == "sí";

                            // Convertir número de llamados a int
                            int numeroLlamados = int.TryParse(worksheet.Cells[row, 13].Text, out int numLlamadosResult) ? numLlamadosResult : 0;

                            // Convertir booleano "Responde Llamados"
                            bool respondeLlamados = worksheet.Cells[row, 14].Text.Trim().ToLower() == "sí";

                            // Convertir fechas de contacto
                            DateTime? fecha1erContacto = DateTime.TryParse(worksheet.Cells[row, 15].Text, out DateTime fecha1erResult) ? fecha1erResult : (DateTime?)null;
                            DateTime? fecha2doContacto = DateTime.TryParse(worksheet.Cells[row, 16].Text, out DateTime fecha2doResult) ? fecha2doResult : (DateTime?)null;
                            DateTime? fecha3erContacto = DateTime.TryParse(worksheet.Cells[row, 17].Text, out DateTime fecha3erResult) ? fecha3erResult : (DateTime?)null;

                            string detalleContacto = worksheet.Cells[row, 18].Text;
                            string reunion1 = worksheet.Cells[row, 19].Text;

                            // Convertir booleano "Solicita Propuesta"
                            bool solicitaPropuesta = worksheet.Cells[row, 20].Text.Trim().ToLower() == "sí";

                            string detalleReunion1 = worksheet.Cells[row, 21].Text;
                            string reunion2 = worksheet.Cells[row, 22].Text;

                            // Convertir booleano "Solicita Propuesta 2"
                            bool solicitaPropuesta2 = worksheet.Cells[row, 23].Text.Trim().ToLower() == "sí";

                            string detalleReunion2 = worksheet.Cells[row, 24].Text;
                            string reunion3 = worksheet.Cells[row, 25].Text;

                            // Convertir booleano "Solicita Propuesta 3"
                            bool solicitaPropuesta3 = worksheet.Cells[row, 26].Text.Trim().ToLower() == "sí";

                            string detalleReunion3 = worksheet.Cells[row, 27].Text;

                            // Convertir fecha de envío de propuesta
                            DateTime? fechaEnvioPropuesta = DateTime.TryParse(worksheet.Cells[row, 28].Text, out DateTime fechaEnvioResult) ? fechaEnvioResult : (DateTime?)null;

                            string aceptaPropuesta = worksheet.Cells[row, 29].Text;
                            string motivos = worksheet.Cells[row, 30].Text;


                            var contacto = new ContactoProspecto
                            {
                                Cargo = cargo,
                                PerfilLinkedin = perfilLinkedin,
                                NombreCompleto = nombreCompleto,
                                Numero = telefono,
                                Email = correo,


                            };
                            var contactoCreado = await _contactoRepository.AddAsync(contacto);
                            // Validar y crear la entidad Prospecto
                            var prospecto = new Prospecto
                            {
                                IdContacto = contactoCreado.Id,
                                CantidadLlamadas = numeroLlamados,
                                Responde = respondeLlamados,
                                IdEstadoProspecto = 1,
                                IdKam = 1,
                                IdCliente = 1,
                                FechaActividad = fechaCarga,


                            };
                            var prospectoCreado = await _prospectoRepository.AddAsync(prospecto);
                            var reunion = new ReunionProspecto
                            {
                                IdProspecto = prospectoCreado.Id,
                                Detalle = detalleReunion1,
                                SolicitaPropuesta = solicitaPropuesta
                            };
                            await _reunionProspectoRepository.AddAsync(reunion);

                            // Guardar prospecto en la base de datos
                            await _prospectoRepository.AddAsync(prospecto);
                        }
                    }
                }

                result.IsSuccess = true;
                result.Message = "Archivo procesado exitosamente.";
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                result.IsSuccess = false;
                result.Message = $"Error al procesar el archivo: {ex.Message}";
            }

            return result;
        }

    }
}
