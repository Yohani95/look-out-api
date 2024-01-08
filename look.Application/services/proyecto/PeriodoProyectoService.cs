using look.Application.interfaces.proyecto;
using look.Application.interfaces.world;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

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

        public PeriodoProyectoService(IPeriodoProyectoRepository periodoProyectoRepository, IUnitOfWork unitOfWork, ITarifarioConvenioRepository tarifarioConvenioService, IProyectoParticipanteRepository proyectoParticipanteService, IProyectoRepository proyectoRepository, IMonedaRepository monedaRepository, IMonedaService monedaService, INovedadesRepository novedadesRepository) : base(periodoProyectoRepository)
        {
            _periodoProyectoRepository = periodoProyectoRepository;
            _unitOfWork = unitOfWork;
            _tarifarioConvenioRepository = tarifarioConvenioService;
            _proyectoParticipanteService = proyectoParticipanteService;
            _proyectoRepository = proyectoRepository;
            _monedaRepository = monedaRepository;
            _monedaService = monedaService;
            _novedadesRepository = novedadesRepository;
        }

        public async Task<List<PeriodoProyecto>> ListByProyecto(int id)
        {
            var periodos = await _periodoProyectoRepository.GetComplete();
            return periodos.Where(p => p.PryId == id).ToList();
        }
        public async Task<ServiceResult> CreateAsync(PeriodoProyecto periodo)
        {
            try
            {
                var periodoExisting = await _periodoProyectoRepository.GetByPeriodoRange(periodo);
                if (periodoExisting != null)
                {
                    _logger.Information("Actualizando periodo");
                    periodoExisting.NumeroProfesionales = periodo.NumeroProfesionales;
                    periodoExisting.estado = periodo.estado;
                    periodoExisting.Monto = await CalcularMontoPeriodo(periodo);
                    await _periodoProyectoRepository.UpdateAsync(periodoExisting);
                }
                else
                {
                    _logger.Information("Creando periodo");
                    periodo.Monto = await CalcularMontoPeriodo(periodo);
                    await _periodoProyectoRepository.AddAsync(periodo);
                }
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
        private async Task<double> CalcularMontoPeriodo(PeriodoProyecto periodo)
        {
            double tarifaConvertida = 0;
            double tarifaTotal = 0;
            Moneda moneda = new Moneda();
            try
            {
                _logger.Information("Calculando monto de periodo");
                var existingProyecto = await _proyectoRepository.GetByIdAsync((int)periodo.PryId);
                if (existingProyecto != null)
                {

                    var proyectoParticipante = await _proyectoParticipanteService.GetParticipanteByIdProAndDate(periodo);
                    foreach (var participante in proyectoParticipante)
                    {
                        var tarifarioConvenio = await _tarifarioConvenioRepository.GetbyIdEntities((int)participante.TarifarioId);
                        moneda = await _monedaRepository.GetByIdAsync((int)existingProyecto.MonId);
                        var novedades = await _novedadesRepository.GetAllAsync();

                        var novedadesFiltrada = novedades
                            .Where(p => p.idProyecto == participante.PryId && 
                                p.idPersona == participante.PerId && p.IdTipoNovedad 
                                != Novedades.ConstantesTipoNovedad.cambioRol && p.fechaInicio>=periodo.FechaPeriodoDesde
                                &&  p.fechaHasta<=periodo.FechaPeriodoHasta).ToList();
                        if (novedadesFiltrada.Count > 0)
                        {
                            //llamar a metodo para calcular dias habiles o mensuales
                            tarifaConvertida = await calculartarifas(existingProyecto, tarifarioConvenio, periodo, moneda, novedadesFiltrada);
                        }
                        else
                        {
                            tarifaConvertida=(double)tarifarioConvenio.TcTarifa;
                        }
                        
                        tarifaTotal = tarifaTotal + tarifaConvertida;
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
                return tarifaTotal;
            }
        }

        /// <summary>
        /// calcula los dias totales del periodo
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="diasFeriados"></param>
        /// <returns></returns>
        static int CalcularDiasTotales(DateTime startDate, DateTime endDate, IEnumerable<Novedades> novedades)
        {
            int diasTotales = 0;

            for (DateTime fecha = startDate; fecha < endDate; fecha = fecha.AddDays(1))
            {
                if (!NovedadesEnRango(fecha, novedades))
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

        async Task<double> calculartarifas(Proyecto proyecto, TarifarioConvenio tarifarioConvenio, PeriodoProyecto periodo, Moneda moneda, IEnumerable<Novedades> novedadesFiltrada)
        {
            double tarifaConvertida = 0;
            double tarifa = 0;
            //double tarifaconvenio = 0;
            int diasTotalesTrabajados = 0;
            double tarifaTotalTrabajado = 0;
            TimeSpan diferencia = (TimeSpan)(periodo.FechaPeriodoHasta.Value.Date - periodo.FechaPeriodoDesde.Value.AddDays(1));

            int diasTotalesPeriodo = 30;

            //int diasHabilesPeriodo = CalcularDiasHabilesSinNovedad((DateTime)periodo.FechaPeriodoDesde, (DateTime)periodo.FechaPeriodoHasta, diasFeriados);

            if (tarifarioConvenio.TcBase == TarifarioConvenio.ConstantesTcBase.Hora)
            {

                diasTotalesTrabajados = CalcularDiasHabiles((DateTime)periodo.FechaPeriodoDesde, (DateTime)periodo.FechaPeriodoHasta, novedadesFiltrada);
                double Horadia = (double)(tarifarioConvenio.TcTarifa * 9);
                tarifaTotalTrabajado = Horadia * diasTotalesTrabajados;
            }
            else
            {
                diasTotalesTrabajados = CalcularDiasTotales((DateTime)periodo.FechaPeriodoDesde, (DateTime)periodo.FechaPeriodoHasta, novedadesFiltrada);
                //Cuando es febrero se calcula si es bisiesto o no
                if(periodo.FechaPeriodoDesde.Value.Month==2 && periodo.FechaPeriodoHasta.Value.Month == 2)
                {
                    var diasTotalesTrabajadosBisiesto = 29;
                    if(periodo.FechaPeriodoDesde.Value.Year % 4 == 0)
                    {
                        diasTotalesPeriodo = diasTotalesTrabajadosBisiesto-1;
                    }
                    if (diasTotalesTrabajados > 28)
                    {
                        diasTotalesTrabajados= diasTotalesPeriodo;
                    }
                }
                //cuando es 31 los dias totales son 30, regla de negocio
                if (diasTotalesTrabajados > 30)
                {
                    diasTotalesTrabajados = 30;
                }
                var tarifaDiario = ((Double)tarifarioConvenio.TcTarifa / diasTotalesPeriodo);// se calcula en base si es mensual
                tarifaTotalTrabajado = tarifaDiario * diasTotalesTrabajados;
            }
             

            //tarifaConvertida = await ConvertirMonedas(moneda.MonNombre, tarifarioConvenio.Moneda.MonNombre, tarifaTotalTrabajado);       
            //return tarifaConvertida;
            return tarifaTotalTrabajado;
        }
        /// <summary>
        /// calcula los dias habiles segun corresponda del periodo
        /// </summary>
        /// <param name="startDate">desde</param>
        /// <param name="endDate">hasta</param>
        /// <param name="diasFeriados"> lista de dias feriados</param>
        /// <returns>retorna un entero</returns>
        static int CalcularDiasHabiles(DateTime startDate, DateTime endDate,IEnumerable<Novedades> novedades)
        {
            int diasHabiles = 0;

            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (EsDiaHabil(fecha) && !NovedadesEnRango(fecha, novedades))
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
            return novedades.Any(n => fecha.Date >= n.fechaInicio.Value.Date && fecha < n.fechaHasta.Value.Date);
                                       //10/04>=01/04 && 10/04<10/04
                                        //true         && false

            //falso
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
    }
}


