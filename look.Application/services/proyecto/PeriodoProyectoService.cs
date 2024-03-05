using look.Application.interfaces.proyecto;
using look.Application.interfaces.world;
using look.domain.dto.admin;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.entities.world;
using look.domain.interfaces.admin;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.unitOfWork;
using look.domain.interfaces.world;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.News.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

namespace look.Application.services.proyecto
{
    public class PeriodoProyectoService : Service<PeriodoProyecto>, IPeriodoProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IPeriodoProyectoRepository _periodoProyectoRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITarifarioConvenioRepository _tarifarioConvenioRepository;
        private readonly IProyectoParticipanteRepository _proyectoParticipanteService;
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IMonedaRepository _monedaRepository;
        private readonly IMonedaService _monedaService;
        private readonly INovedadesRepository _novedadesRepository;
        private PeriodoProfesionales periodoProfesionales;
        private readonly IPeriodoProfesionalesRepository _periodoProfesionalesRepository;

        public PeriodoProyectoService(IPeriodoProyectoRepository periodoProyectoRepository, IUnitOfWork unitOfWork,
            ITarifarioConvenioRepository tarifarioConvenioService,
            IProyectoParticipanteRepository proyectoParticipanteService,
            IProyectoRepository proyectoRepository, IMonedaRepository monedaRepository,
            IMonedaService monedaService, INovedadesRepository novedadesRepository, IPeriodoProfesionalesRepository periodoProfesionalesRepository) : base(periodoProyectoRepository)
        {
            _periodoProyectoRepository = periodoProyectoRepository;
            _unitOfWork = unitOfWork;
            _tarifarioConvenioRepository = tarifarioConvenioService;
            _proyectoParticipanteService = proyectoParticipanteService;
            _proyectoRepository = proyectoRepository;
            _monedaRepository = monedaRepository;
            _monedaService = monedaService;
            _novedadesRepository = novedadesRepository;
            _periodoProfesionalesRepository = periodoProfesionalesRepository;
        }

        public async Task<List<PeriodoProyecto>> ListByProyecto(int id)
        {
            var periodos = await _periodoProyectoRepository.GetComplete();
            return periodos.Where(p => p.PryId == id).ToList();
        }
        public async Task<ServiceResult> CreateAsync(PeriodoProyecto periodo)
        {
            List<PeriodoProfesionales> periodoProfesionales = new List<PeriodoProfesionales>();
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var periodoExisting = await _periodoProyectoRepository.GetByPeriodoRange(periodo);
                if (periodoExisting != null)
                {
                    _logger.Information("Actualizando periodo");
                    //solucion 
                    periodo.id = periodoExisting.id;
                    periodoExisting.NumeroProfesionales = periodo.NumeroProfesionales;
                    periodoExisting.estado = periodo.estado;
                    periodoExisting.Monto = await CalcularMontoPeriodo(periodo, periodoProfesionales);
                    periodoExisting.DiasTotal = periodo.DiasTotal;
                    await _periodoProyectoRepository.UpdateAsync(periodoExisting);
                }
                else
                {
                    _logger.Information("Creando periodo");
                    periodo.Monto = await CalcularMontoPeriodo(periodo, periodoProfesionales);
                    var periodoCreated = await _periodoProyectoRepository.AddAsync(periodo);
                    foreach (var item in periodoProfesionales)
                    {
                        item.IdPeriodo = periodoCreated.id; ;
                        await _periodoProfesionalesRepository.AddAsync(item);
                    }
                }
                await _unitOfWork.CommitAsync();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                    MessageCode = ServiceResultMessage.Success
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Error interno del servidor: " + ex.Message);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
                throw;
            }
        }
        public async Task<List<PeriodoProyecto>> ListComplete()
        {
            return await _periodoProyectoRepository.GetComplete();
        }
        #region "logica de calcular monto"
        /// <summary>
        /// calcula el monto segun la novedad de los participantes 
        /// </summary>
        /// <param name="periodo">espera un periodo</param>
        /// <returns>retorna el monto total del periodo a facturar</returns>
        private async Task<double> CalcularMontoPeriodo(PeriodoProyecto periodo, List<PeriodoProfesionales> listPeriodoProfesionales)
        {
            double tarifaConvertida = 0;
            double tarifaTotal = 0;
            Moneda moneda = new Moneda();
            var profesionales = await _periodoProfesionalesRepository.GetAllAsync();
            try
            {
                _logger.Information("Calculando monto de periodo");
                var existingProyecto = await _proyectoRepository.GetByIdAsync((int)periodo.PryId);
                if (existingProyecto != null)
                {

                    var proyectoParticipante = await _proyectoParticipanteService.GetParticipanteByIdProAndDate(periodo);
                    foreach (var participante in proyectoParticipante)
                    {
                        periodoProfesionales = new PeriodoProfesionales();
                        periodoProfesionales.IdParticipante = participante.PpaId;

                        var tarifarioConvenio = await _tarifarioConvenioRepository.GetbyIdEntities((int)participante.TarifarioId);
                        moneda = await _monedaRepository.GetByIdAsync((int)existingProyecto.MonId);
                        var novedades = await _novedadesRepository.GetComplete();

                        var novedadesFiltrada = novedades
                        .Where(p => p.idProyecto == participante.PryId &&
                        p.idPersona == participante.PerId &&
                        p.IdTipoNovedad != Novedades.ConstantesTipoNovedad.cambioRol && p.TipoNovedades.Descuento!=0&&
                        ((p.fechaInicio <= periodo.FechaPeriodoHasta && p.fechaInicio >= periodo.FechaPeriodoDesde) || // Novedad inicia dentro del período
                        (p.fechaHasta <= periodo.FechaPeriodoHasta && p.fechaHasta >= periodo.FechaPeriodoDesde) || // Novedad termina dentro del período
                        (p.fechaInicio <= periodo.FechaPeriodoDesde && p.fechaHasta >= periodo.FechaPeriodoHasta))) // Novedad cubre todo el período
                        .ToList();

                        //llamar a metodo para calcular dias habiles o mensuales
                        tarifaConvertida = await calculartarifas(
                                                    tarifarioConvenio, periodo, moneda, novedadesFiltrada, participante,existingProyecto);
                        tarifaTotal = tarifaTotal + tarifaConvertida;
                        if (periodo.id != null && periodo.id != 0)
                        {
                            periodoProfesionales.IdPeriodo = periodo.id;
                            var periodoProfesional = profesionales.Where(p => p.IdParticipante == participante.PpaId && p.IdPeriodo == periodo.id).FirstOrDefault();
                            if (periodoProfesional != null)
                            {
                                periodoProfesional.IdPeriodo = periodoProfesional.IdPeriodo;
                                periodoProfesional.DiasTrabajados = periodoProfesionales.DiasTrabajados;
                                periodoProfesional.DiasAusentes = periodoProfesionales.DiasAusentes;
                                periodoProfesional.DiasVacaciones = periodoProfesionales.DiasVacaciones;
                                periodoProfesional.DiasLicencia = periodoProfesionales.DiasLicencia;
                                periodoProfesional.DiasFeriados = periodoProfesionales.DiasFeriados;
                                periodoProfesional.IdParticipante = periodoProfesionales.IdParticipante;
                                periodoProfesional.IdPeriodo = periodoProfesionales.IdPeriodo;
                                periodoProfesional.MontoDiario = periodoProfesionales.MontoDiario;
                                periodoProfesional.MontoTotalPagado = periodoProfesionales.MontoTotalPagado;
                                await _periodoProfesionalesRepository.UpdateAsync(periodoProfesional);
                            }
                            else
                            {
                                await _periodoProfesionalesRepository.AddAsync(periodoProfesionales);
                            }
                        }

                        listPeriodoProfesionales.Add(periodoProfesionales);

                    }
                }
                var culture = System.Globalization.CultureInfo.InvariantCulture;
                string numeroFormateado = tarifaTotal.ToString("0.###", culture);
                _logger.Information("Monto calculado de periodo :" + tarifaTotal + ", moneda :" + moneda.MonNombre);
                return Double.Parse(numeroFormateado, culture);
            }
            catch (Exception ex)
            {
                _logger.Error("Error interno del servidor al calcular: " + ex.Message);
                throw new Exception("Error interno del servidor al calcular: " + ex.Message);
            }
        }
        /// <summary>
        /// calcula los dias totales del periodo
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="novedades"></param>
        /// <param name="participante"></param>
        /// <returns></returns>
        static int CalcularDiasTotales(DateTime startDate, DateTime endDate, List<Novedades> novedades, ProyectoParticipante participante)
        {
            int diasTotales = 0;

            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (!NovedadesEnRango(fecha, novedades) && CalcularFechaInicioParticipante(fecha, participante))
                {
                    diasTotales++;
                }
            }

            return diasTotales;
        }
        static int CalcularDiasHabilesSinNovedad(DateTime startDate, DateTime endDate, List<DateTime> diasFeriados)
        {
            int diasHabiles = 0;

            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (!EsDiaFeriado(fecha, diasFeriados) && fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasHabiles++;
                }
            }

            return diasHabiles;
        }

        async Task<double> calculartarifas(TarifarioConvenio tarifarioConvenio, PeriodoProyecto periodo, Moneda moneda, List<Novedades> novedadesFiltrada, ProyectoParticipante participante,Proyecto existingProyecto)
        {

            double tarifaConvertida = 0;
            double tarifa = 0;
            //double tarifaconvenio = 0;
            int diasTotalesTrabajados = 0;
            double tarifaTotalTrabajado = 0;
            double tarifaDiario = 0;

            TimeSpan diferencia = (TimeSpan)(periodo.FechaPeriodoHasta.Value.Date.AddDays(1) - periodo.FechaPeriodoDesde.Value.Date);

            int diasTotalesPeriodo = diferencia.Days;

            if (existingProyecto.FacturacionDiaHabil!=0)
            {

                var diasFeriados = await ObtenerDiasFeriados(periodo.FechaPeriodoDesde.Value.Year);
                diasTotalesTrabajados = CalcularDiasHabiles((DateTime)periodo.FechaPeriodoDesde, (DateTime)periodo.FechaPeriodoHasta, novedadesFiltrada, diasFeriados, participante);
                diasTotalesPeriodo = CalcularDiasHabilSinNovedad((DateTime)periodo.FechaPeriodoDesde, (DateTime)periodo.FechaPeriodoHasta, diasFeriados);
                if (tarifarioConvenio.TcBase == TarifarioConvenio.ConstantesTcBase.Hora) {
                    //este caso es evaluado exepcionalmente para el pais peru
                    var horas =existingProyecto.PaisId==2? 8 : 9;
                    if (horas == 8 && tarifaTotalTrabajado>=20)
                    {
                        tarifaDiario = (double)(tarifarioConvenio.TcTarifa);
                        tarifaTotalTrabajado = (tarifaDiario/8) * 160;
                    }
                    else
                    {
                        tarifaDiario = (double)(tarifarioConvenio.TcTarifa * horas);
                        tarifaTotalTrabajado = tarifaDiario * diasTotalesTrabajados;
                    }

                }
                else
                {
                    tarifaDiario = (double)(tarifarioConvenio.TcTarifa / diasTotalesPeriodo);
                    tarifaTotalTrabajado = tarifaDiario * diasTotalesTrabajados;
                }
            }
            else
            {
                //se calcula en base si es mensual  
                diasTotalesTrabajados = CalcularDiasTotales((DateTime)periodo.FechaPeriodoDesde, (DateTime)periodo.FechaPeriodoHasta, novedadesFiltrada, participante);
                tarifaDiario = ((Double)tarifarioConvenio.TcTarifa / diasTotalesPeriodo);
                tarifaTotalTrabajado = tarifaDiario * diasTotalesTrabajados;
            }

            periodoProfesionales.DiasTrabajados = diasTotalesTrabajados;
            periodoProfesionales.DiasAusentes = diasTotalesPeriodo - diasTotalesTrabajados;
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            string numeroFormateado = tarifaDiario.ToString("0.##", culture);
            periodoProfesionales.MontoDiario = Double.Parse(numeroFormateado, culture);
            periodoProfesionales.MontoTotalPagado = Double.Parse(tarifaTotalTrabajado.ToString("0.##", culture), culture);
            periodo.DiasTotal = diasTotalesPeriodo;
            return tarifaTotalTrabajado;
        }
        /// <summary>
        /// calcula los dias habiles segun corresponda del periodo
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="novedades"></param>
        /// <param name="feriados"></param>
        /// <param name="participante"></param>
        /// <returns></returns>
        static int CalcularDiasHabiles(DateTime startDate, DateTime endDate, IEnumerable<Novedades> novedades, List<DateTime> feriados, ProyectoParticipante participante)
        {
            int diasHabiles = 0;
            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (!EsDiaFeriado(fecha, feriados) && EsDiaHabil(fecha))
                {
                    if (!NovedadesEnRango(fecha, novedades) && CalcularFechaInicioParticipante(fecha, participante))
                    {
                        diasHabiles++;
                    }
                }
            }

            return diasHabiles;
        }
        static int CalcularDiasHabilSinNovedad(DateTime startDate, DateTime endDate,  List<DateTime> feriados)
        {
            int diasHabiles = 0;
            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (!EsDiaFeriado(fecha, feriados) && EsDiaHabil(fecha))
                {
                        diasHabiles++;
                 }
            }

            return diasHabiles;
        }
        /// <summary>
        /// obtiene los dias feriados por año
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="diasFeriados"></param>
        /// <returns></returns>
        static bool EsDiaFeriado(DateTime fecha, List<DateTime> diasFeriados)
        {
            if (EsDiaHabil(fecha))
            {
                return diasFeriados.Contains(fecha.Date);
            }
            return false;
        }
        /// <summary>
        /// verifica si es dia habil 
        /// </summary>
        /// <param name="fecha">espera una fecha</param>
        /// <returns>retorna true o false </returns>
        static bool EsDiaHabil(DateTime fecha)
        {
            // Verifica si el día no es sábado ni domingo
            return fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday;
        }
        /// <summary>
        /// obtiene si hubo una novedad dentro del periodo
        /// </summary>
        /// <param name="fecha">fecha</param>
        /// <param name="novedades"> lista de novedad </param>
        /// <returns>retorna un true si encuentra una novedad</returns>
        static bool NovedadesEnRango(DateTime fecha, IEnumerable<Novedades> novedades)
        {
            var ultimasTerminosServicio = new Dictionary<int, Novedades>();

            foreach (var novedad in novedades)
            {
                if (novedad.IdTipoNovedad == Novedades.ConstantesTipoNovedad.TerminoServicio)
                {
                    // Si ya hay una novedad de TerminoServicio con el mismo ID, la reemplazamos
                    ultimasTerminosServicio[novedad.id] = novedad;
                }
                else if (!ultimasTerminosServicio.ContainsKey(novedad.id))
                {
                    // Si no es de tipo TerminoServicio o ya tenemos una de TerminoServicio para este ID, la agregamos
                    ultimasTerminosServicio.Add(novedad.id, novedad);
                }
            }

            return ultimasTerminosServicio.Values.Any(n => fecha.Date >= n.fechaInicio.Value.Date
                                                        && fecha.Date <= n.fechaHasta.Value.Date);
        }

        /// <summary>
        /// calcula la fecha de inicio y de termino de un participante
        /// </summary>
        /// <param name="fecha">fecha del incio del periodo</param>
        /// <param name="participante"> objeto participante</param>
        /// <returns>retornar un boolean segun el caso</returns>
        static bool CalcularFechaInicioParticipante(DateTime fecha, ProyectoParticipante participante)
        {
            if (participante.FechaTermino != null)
            {
                return fecha.Date >= participante.FechaAsignacion.Value.Date && fecha.Date <= participante.FechaTermino.Value.Date;
            }
            return fecha.Date >= participante.FechaAsignacion.Value.Date;
        }
        /// <summary>
        /// obtiene los dias feriados de la api
        /// </summary>
        /// <param name="year">espera el año</param>
        /// <returns>retorna una lista de fechas</returns>
        static async Task<List<DateTime>> ObtenerDiasFeriados(int year)
        {
            string url = "https://apis.digital.gob.cl/fl/feriados/" + year;
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    List<DateTime> feriadosfechas = new List<DateTime>();
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string contenido = await response.Content.ReadAsStringAsync();
                    List<Feriado> feriados = JsonConvert.DeserializeObject<List<Feriado>>(contenido);
                    foreach (var fecha in feriados)
                    {
                        feriadosfechas.Add(DateTime.Parse(fecha.Fecha));
                    }
                    return feriadosfechas;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error al obtener los feriados: {e.Message}");
                    return null;
                }
            }
        }
        /// <summary>
        /// obtiene uf de api https://mindicador.cl/api/uf
        /// </summary>
        /// <param name="year">espera fecha formato {DD-MM-YYYY}</param>
        /// <returns>retorna un double </returns>
        static async Task<double> ObtenerUf(string date)
        {
            int añoActual = DateTime.Now.Year;
            int diaActual = DateTime.Now.Day - 1;
            int mesActual = DateTime.Now.Month;
            string año = diaActual + "-" + mesActual + "-" + añoActual;
            string url = "https://mindicador.cl/api/uf/" + año;

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {

                    string apiUrl = url;

                    string jsonResult = await ObtenerJson(apiUrl);

                    double valorUF = ObtenerValorUF(jsonResult);
                    return valorUF;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error al obtener los feriados: {e.Message}");
                    return 0;
                }
            }
        }
        /// <summary>
        /// obtiene json de api UF "https://mindicador.cl/api/uf
        /// </summary>
        /// <param name="apiUrl">espera url</param>
        /// <returns>retorna un cuerpo de resultado</returns>
        static async Task<string> ObtenerJson(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error al obtener los datos: {e.Message}");
                    return null;
                }
            }
        }
        /// <summary>
        /// deserializa el json dela api uf
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns></returns>
        static double ObtenerValorUF(string jsonResult)
        {
            try
            {
                // Deserializa la respuesta JSON y obtiene el valor de la serie
                var resultado = JsonConvert.DeserializeObject<Resultado>(jsonResult);
                double valorUF = resultado.Serie[0].Valor;
                return valorUF;
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error al deserializar los datos: {e.Message}");
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoMoneda"></param>
        /// <param name="moneda"></param>
        /// <param name="tarifa"></param>
        /// <returns></returns>
        private async Task<double> ConvertirMonedas(string tipoMoneda, string tarifarioMoneda, double tarifa)
        {
            try
            {
                if (tipoMoneda.Equals("UF"))
                {
                    DateTime fechaHoy = DateTime.Now;
                    string fechaFormateada = fechaHoy.ToString("dd-MM-yyyy");
                    var valorUf = await ObtenerUf(fechaFormateada);

                    if (!tarifarioMoneda.Equals("CLP"))
                    {
                        var resultCLP = await _monedaService.consultaMonedaConvertida((string)tarifarioMoneda, "CLP", tarifa);
                        dynamic json = JObject.Parse(resultCLP);
                        resultCLP = json.MonedaConvertida;
                        var culture = System.Globalization.CultureInfo.InvariantCulture;

                        var result = Double.Parse(resultCLP, culture) / valorUf;
                        var resultadoFormateado = result.ToString("0.###", culture);

                        return Double.Parse(resultadoFormateado, culture);
                    }
                    return tarifa / valorUf;
                }
                else
                {
                    var result = await _monedaService.consultaMonedaConvertida(tipoMoneda, (string)tarifarioMoneda, (int)tarifa);
                    dynamic json = JObject.Parse(result);
                    result = json.MonedaConvertida;
                    return Double.Parse(result) * tarifa;
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error al deserializar los datos: {e.Message}");
                throw;
            }
        }
        #endregion
        public async Task<PeriodoProyecto> GetPeriodoProyectoById(int id)
        {
            return await _periodoProyectoRepository.GetPeriodoProyectoById(id);
        }
    }
}


