using look.Application.interfaces.prospecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.prospecto;
using look.domain.interfaces.admin;
using look.domain.interfaces.cuentas;
using look.domain.interfaces.oportunidad;
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
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IAreaServicioOportunidadRepository _areaServicioOportunidadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger = Logger.GetLogger();

        public ProspectoService(IProspectoRepository prospectoRepository, IContactoProspectoRepository contactoProspectoRepository,
            ILlamadaProspectoRepository llamadaProspectoRepository, IReunionProspectoRepository reunionProspectoRepository, IUnitOfWork unitOfWork,
            IClienteRepository clienteRepository, IAreaServicioOportunidadRepository areaServicioOportunidadRepository, IPersonaRepository personaRepository) : base(prospectoRepository)
        {
            _prospectoRepository = prospectoRepository;
            _contactoRepository = contactoProspectoRepository;
            _reunionProspectoRepository = reunionProspectoRepository;
            _llamadaProspectoRepository = llamadaProspectoRepository;
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
            _areaServicioOportunidadRepository = areaServicioOportunidadRepository;
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
                // Establecer el contexto de la licencia de EPPlus
                //esta licencia es para uso no comercial 
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                                // Aquí asignamos el Id de TipoContactoProspecto, que es un int, a IdTipo que es int?
                                IdTipo = clasificacion == "TIR1"
                                    ? (int?)TipoContactoProspecto.TIR1.Id
                                    : clasificacion == "TIR2"
                                    ? (int?)TipoContactoProspecto.TIR2.Id
                                    : clasificacion == "TIR3"
                                    ? (int?)TipoContactoProspecto.TIR3.Id
                                    : null, // Si no es TIR1, TIR2 o TIR3, será nulo

                            };
                            //agregar contacto de prospecto
                            var contactoCreado = await _contactoRepository.AddAsync(contacto);
                            //buscar kam o asignado
                            var kamId = (await _personaRepository.GetAllByType(TipoPersona.Kam.Id))
                                        .FirstOrDefault(kam => kam.PerNombres != null && kam.PerNombres.Contains(asignacion))?.Id;
                            var clienteId = (await _clienteRepository.GetAllAsync())
                                        .FirstOrDefault(c => c.CliNombre != null && c.CliNombre.Contains(empresa))?.CliId;
                            // Validar y crear la entidad Prospecto
                            var prospecto = new Prospecto
                            {
                                IdContacto = contactoCreado.Id,
                                CantidadLlamadas = numeroLlamados,
                                Responde = respondeLlamados,
                                IdEstadoProspecto = EstadoProspecto.Pendiente.Id,
                                IdKam = kamId,
                                IdCliente = clienteId,
                                FechaActividad = fechaCarga,

                            };
                            var prospectoCreado = await _prospectoRepository.AddAsync(prospecto);

                            var llamadaProspecto = new LlamadaProspecto
                            {
                                IdProspecto = prospectoCreado.Id,
                                FechaCreacion = fecha1erContacto,
                                Detalle = detalleContacto,
                                RespondeLlamada = respondeLlamados
                            };
                            await _llamadaProspectoRepository.AddAsync(llamadaProspecto);
                            var reunion = new ReunionProspecto
                            {
                                IdProspecto = prospectoCreado.Id,
                                Detalle = detalleReunion1,
                                SolicitaPropuesta = solicitaPropuesta,
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
