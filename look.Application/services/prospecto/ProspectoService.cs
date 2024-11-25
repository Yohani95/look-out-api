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
            var errores = new List<string>(); // Lista para almacenar los errores

            try
            {
                if (file == null || file.Length == 0)
                {
                    result.IsSuccess = false;
                    result.Message = "El archivo proporcionado está vacío o no es válido.";
                    result.MessageCode = ServiceResultMessage.InvalidInput;
                    return result;
                }

                // Establecer el contexto de la licencia de EPPlus para uso no comercial
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
                            try
                            {
                                var contacto = await ProcesarContactoAsync(worksheet, row, errores); // Procesar y capturar errores
                                var prospecto = await ProcesarProspectoAsync(worksheet, row, contacto, errores);
                                await ProcesarLlamadasAsync(worksheet, row, prospecto.Id, errores);
                                await ProcesarReunionesAsync(worksheet, row, prospecto.Id, errores);
                            }
                            catch (Exception ex)
                            {
                                errores.Add($"Error en la fila {row}: {ex.Message}");
                            }
                        }
                    }
                }

                if (errores.Any())
                {
                    // Si hubo errores, no hacemos commit y los devolvemos en el resultado
                    await _unitOfWork.RollbackAsync();
                    result.IsSuccess = false;
                    result.Message = "Hubo errores durante el procesamiento del archivo.";
                    result.MessageCode = ServiceResultMessage.Conflict;
                    result.Errors = errores;
                }
                else
                {
                    // Si no hubo errores, hacemos commit
                    result.IsSuccess = true;
                    result.Message = "Archivo procesado exitosamente.";
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                result.IsSuccess = false;
                result.Message = $"Error al procesar el archivo: {ex.Message}";
            }

            return result;
        }
        private async Task<ContactoProspecto> ProcesarContactoAsync(ExcelWorksheet worksheet, int row, List<string> errores)
        {
            // Procesar datos del contacto
            string cargo = worksheet.Cells[row, 5].Text;
            string perfilLinkedin = worksheet.Cells[row, 8].Text;
            string nombreCompleto = worksheet.Cells[row, 3].Text;
            string correo = worksheet.Cells[row, 6].Text.Trim();

            // Leer el teléfono como texto (para evitar notación científica)
            string telefonoTexto = worksheet.Cells[row, 7].Text.Trim();
            string telefono = telefonoTexto;

            if (string.IsNullOrEmpty(nombreCompleto))
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(3)} ({ObtenerCabecera(worksheet, 3)}): El nombre completo no puede estar vacío.");
            }

            if (!IsValidEmail(correo))
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(6)} ({ObtenerCabecera(worksheet, 6)}): El correo electrónico es inválido.");
            }

            // Manejar el número de teléfono, evitando notación científica
            if (double.TryParse(telefonoTexto, out double telefonoNumero))
            {
                telefono = telefonoNumero.ToString("F0", System.Globalization.CultureInfo.InvariantCulture); // Convertir sin decimales
            }
            else if (telefonoTexto.Contains("E+"))
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(7)} ({ObtenerCabecera(worksheet, 7)}): El número de teléfono '{telefonoTexto}' no es válido.");
            }

            // Validar que el número no esté vacío y que sea válido
            if (!string.IsNullOrEmpty(telefono) && !EsNumeroDeTelefonoValido(telefono))
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(7)} ({ObtenerCabecera(worksheet, 7)}): El número de teléfono '{telefono}' no es válido.");
            }

            var contacto = new ContactoProspecto
            {
                Cargo = cargo,
                PerfilLinkedin = perfilLinkedin,
                NombreCompleto = nombreCompleto,
                Numero = !string.IsNullOrEmpty(telefono) ? telefono : null, // Permitir teléfono nulo si está vacío
                Email = correo,
                IdTipo = ObtenerIdTipoContacto(worksheet.Cells[row, 4].Text)
            };

            return await _contactoRepository.AddAsync(contacto);
        }

        private async Task<Prospecto> ProcesarProspectoAsync(ExcelWorksheet worksheet, int row, ContactoProspecto contacto, List<string> errores)
        {
            string empresa = worksheet.Cells[row, 1].Text;
            string asignacion = worksheet.Cells[row, 11].Text;
            // Validación del número de llamados (columna 13)
            string numeroLlamadosTexto = worksheet.Cells[row, 13].Text;
            int numeroLlamados = 0; // Valor por defecto si está vacío

            if (!string.IsNullOrWhiteSpace(numeroLlamadosTexto))
            {
                if (!int.TryParse(numeroLlamadosTexto, out numeroLlamados))
                {
                    errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(13)} ({ObtenerCabecera(worksheet, 13)}): El número de llamados es inválido.");
                }
            }

            bool respondeLlamados;
            var respondeTexto = worksheet.Cells[row, 14].Text.Trim().ToLower();
            if (respondeTexto == "sí" || respondeTexto == "si" || respondeTexto == "s" || respondeTexto == "y")
            {
                respondeLlamados = true;
            }
            else if (respondeTexto == "No" || string.IsNullOrEmpty(respondeTexto) || respondeTexto == "no" || respondeTexto == "n")
            {
                respondeLlamados = false;
            }
            else
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(14)} ({ObtenerCabecera(worksheet, 14)}): El valor debe ser 'Si' o 'No'.");
                respondeLlamados = false; // Valor por defecto
            }

            // Validación de fecha de carga (columna 10)
            var (fechaCarga, errorFecha) = ConvertirFecha(worksheet.Cells[row, 10].Text, row, 10);
            if (errorFecha != null)
            {
                errores.Add(errorFecha);
            }


            // Buscar el kamId en la base de datos
            var kamId = (await _personaRepository.GetAllByType(TipoPersona.Kam.Id))
                        .FirstOrDefault(kam => kam.PerNombres != null && kam.PerNombres.Contains(asignacion))?.Id;

            if (kamId == null)
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(11)} ({ObtenerCabecera(worksheet, 11)}): No se encontró un KAM con la asignación '{asignacion}'.");
            }

            // Buscar el clienteId en la base de datos
            var clienteId = (await _clienteRepository.GetAllAsync())
                            .FirstOrDefault(c => c.CliNombre != null && c.CliNombre.Contains(empresa))?.CliId;

            if (clienteId == null)
            {
                errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(1)} ({ObtenerCabecera(worksheet, 1)}): No se encontró un cliente con el nombre '{empresa}'.");
            }

            var prospecto = new Prospecto
            {
                IdContacto = contacto.Id,
                CantidadLlamadas = numeroLlamados,
                Responde = respondeLlamados,
                IdEstadoProspecto = EstadoProspecto.Pendiente.Id,
                IdKam = kamId,
                IdCliente = clienteId,
                FechaActividad = fechaCarga,
            };

            return await _prospectoRepository.AddAsync(prospecto);
        }

        private async Task ProcesarLlamadasAsync(ExcelWorksheet worksheet, int row, int prospectoId, List<string> errores)
        {
            // Capturamos las tres posibles llamadas
            var llamadas = new[]
            {
                new { FechaTexto = worksheet.Cells[row, 15].Text, Columna = 15, Detalle = worksheet.Cells[row, 18].Text, RespondeLlamada = worksheet.Cells[row, 14].Text.Trim().ToLower() == "sí" },
                new { FechaTexto = worksheet.Cells[row, 16].Text, Columna = 16, Detalle = worksheet.Cells[row, 18].Text, RespondeLlamada = worksheet.Cells[row, 14].Text.Trim().ToLower() == "sí" },
                new { FechaTexto = worksheet.Cells[row, 17].Text, Columna = 17, Detalle = worksheet.Cells[row, 18].Text, RespondeLlamada = worksheet.Cells[row, 14].Text.Trim().ToLower() == "sí" }
            };

            // Procesar cada llamada
            for (int i = 0; i < llamadas.Length; i++)
            {
                var llamada = llamadas[i];

                // Convertir la fecha y capturar errores si los hay
                var (Fecha, Error) = ConvertirFecha(llamada.FechaTexto, row, llamada.Columna);

                if (Error != null)
                {
                    // Capturar el error si la fecha es inválida
                    errores.Add(Error);
                    continue; // Saltar a la siguiente llamada
                }

                if (Fecha.HasValue)
                {
                    // Si la fecha es válida, procesamos la llamada
                    var llamadaProspecto = new LlamadaProspecto
                    {
                        IdProspecto = prospectoId,
                        FechaCreacion = Fecha,
                        Detalle = llamada.Detalle,
                        RespondeLlamada = llamada.RespondeLlamada
                    };

                    await _llamadaProspectoRepository.AddAsync(llamadaProspecto);
                }
            }
        }


        private async Task ProcesarReunionesAsync(ExcelWorksheet worksheet, int row, int prospectoId, List<string> errores)
        {
            var reuniones = new[]
            {
                new { SolicitaPropuesta = worksheet.Cells[row, 20].Text.Trim().ToLower(), Detalle = worksheet.Cells[row, 21].Text },
                new { SolicitaPropuesta = worksheet.Cells[row, 23].Text.Trim().ToLower(), Detalle = worksheet.Cells[row, 24].Text },
                new { SolicitaPropuesta = worksheet.Cells[row, 26].Text.Trim().ToLower(), Detalle = worksheet.Cells[row, 27].Text }
            };

            for (int i = 0; i < reuniones.Length; i++)
            {
                var reunion = reuniones[i];

                // Validación de "Solicita Propuesta"
                bool solicitaPropuesta;
                if (reunion.SolicitaPropuesta == "sí" || reunion.SolicitaPropuesta == "si" || reunion.SolicitaPropuesta == "s" || reunion.SolicitaPropuesta == "y")
                {
                    solicitaPropuesta = true;
                }
                else if (reunion.SolicitaPropuesta == "No" || string.IsNullOrEmpty(reunion.SolicitaPropuesta) || reunion.SolicitaPropuesta == "no" || reunion.SolicitaPropuesta == "n")
                {
                    solicitaPropuesta = false;
                }
                else
                {
                    errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(20 + (i * 3))} ({ObtenerCabecera(worksheet, 20 + (i * 3))}): El valor de 'Solicita Propuesta' debe ser 'Si', 'No' o estar vacío.");
                    continue; // Saltar al siguiente si no es válido
                }

                // Validar Detalle de Reunión (opcional pero si está presente debe ser correcto)
                if (!string.IsNullOrEmpty(reunion.Detalle) && reunion.Detalle.Length > 500)
                {
                    errores.Add($"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(21 + (i * 3))} ({ObtenerCabecera(worksheet, 21 + (i * 3))}): El detalle de la reunión excede los 500 caracteres.");
                    continue; // Saltar al siguiente si no es válido
                }

                var nuevaReunion = new ReunionProspecto
                {
                    IdProspecto = prospectoId,
                    Detalle = reunion.Detalle,
                    SolicitaPropuesta = solicitaPropuesta
                };

                await _reunionProspectoRepository.AddAsync(nuevaReunion);
            }
        }


        private (DateTime? Fecha, string? Error) ConvertirFecha(string fechaTexto, int row, int columna)
        {
            // Reemplazar guiones por barras para unificar el formato
            fechaTexto = fechaTexto.Replace("-", "/");

            // Eliminar el punto al final del mes abreviado si existe
            fechaTexto = fechaTexto.Replace(".", "").Trim();

            // Especifica una lista extensa de formatos de fecha que esperas en Chile y formatos comunes
            string[] formatosPermitidos = {
        "dd/MM/yyyy", "d/M/yyyy", "dd/MM/yy", "d/M/yy", // Formatos estándar con barras
        "MM/dd/yyyy", "M/d/yyyy", "M/d/yy", "MM/dd/yy", // Formatos estándar de EE.UU.
        "yyyy/MM/dd", "yy/MM/dd", // Formatos con año al inicio
        "MMM/yyyy", "MMM/yy", // Mes abreviado y año
        "d-M-yyyy", "dd-MM-yyyy", "d-M-yy", "dd-MM-yy", // Formatos con guiones
        "M-d-yyyy", "MM-dd-yyyy", "M-d-yy", "MM-dd-yy", // Formatos de EE.UU. con guiones
        "yyyy-MM-dd", "yy-MM-dd", // Formatos con año al inicio y guiones
        "MMM-yyyy", "MMM-yy" // Mes abreviado con guiones
    };

            // Intentar convertir la fecha en base a los formatos permitidos
            if (DateTime.TryParseExact(fechaTexto, formatosPermitidos, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime fechaResult))
            {
                return (fechaResult, null); // Fecha válida
            }
            else if (string.IsNullOrWhiteSpace(fechaTexto))
            {
                return (null, null); // La celda está vacía, no hay error, pero tampoco fecha
            }
            else
            {
                return (null, $"Error en la fila {row}, columna {ConvertirNumeroColumnaALetra(columna)} : La fecha '{fechaTexto}' no es válida."); // Error de formato
            }
        }

        private int? ObtenerIdTipoContacto(string clasificacion)
        {
            return clasificacion switch
            {
                "TIR1" => TipoContactoProspecto.TIR1.Id,
                "TIR2" => TipoContactoProspecto.TIR2.Id,
                "TIR3" => TipoContactoProspecto.TIR3.Id,
                _ => null,
            };
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true; // Se permite que sea nulo o vacío
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string ConvertirNumeroColumnaALetra(int numeroColumna)
        {
            string letraColumna = "";
            while (numeroColumna > 0)
            {
                int mod = (numeroColumna - 1) % 26;
                letraColumna = Convert.ToChar(65 + mod) + letraColumna;
                numeroColumna = (numeroColumna - mod) / 26;
            }
            return letraColumna;
        }
        private string ObtenerCabecera(ExcelWorksheet worksheet, int columna)
        {
            return worksheet.Cells[1, columna].Text; // Suponemos que las cabeceras están en la fila 1
        }
        // Validación opcional para verificar si el número de teléfono es válido
        private bool EsNumeroDeTelefonoValido(string telefono)
        {
            // Eliminar espacios en blanco al principio y al final
            telefono = telefono?.Trim();

            // Si el teléfono es nulo o vacío, considerarlo válido
            if (string.IsNullOrEmpty(telefono))
            {
                return true;
            }

            // Definir los caracteres permitidos en un número de teléfono (incluye '+' al principio, dígitos, espacios, guiones y paréntesis)
            return telefono.StartsWith("+")
                ? telefono.Length > 1 && telefono.Substring(1).All(c => char.IsDigit(c) || c == ' ' || c == '-' || c == '(' || c == ')') // Si empieza con '+', el resto puede ser dígitos o caracteres permitidos
                : telefono.All(c => char.IsDigit(c) || c == ' ' || c == '-' || c == '(' || c == ')'); // Si no empieza con '+', verificar dígitos y caracteres permitidos
        }

    }
}
