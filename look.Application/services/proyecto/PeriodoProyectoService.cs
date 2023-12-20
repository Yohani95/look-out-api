using look.Application.interfaces.proyecto;
using look.Application.interfaces.world;
using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces.admin;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.unitOfWork;
using look.domain.interfaces.world;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace look.Application.services.proyecto
{
    public class PeriodoProyectoService: Service<PeriodoProyecto>, IPeriodoProyectoService
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

        public PeriodoProyectoService(IPeriodoProyectoRepository periodoProyectoRepository,IUnitOfWork unitOfWork, ITarifarioConvenioRepository tarifarioConvenioService,IProyectoParticipanteRepository proyectoParticipanteService,IProyectoRepository proyectoRepository,IMonedaRepository monedaRepository,IMonedaService monedaService,INovedadesRepository novedadesRepository) : base(periodoProyectoRepository)
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
            var periodos= await _periodoProyectoRepository.GetComplete();
            return periodos.Where(p=>p.PryId==id).ToList();
        }
        public async Task<ServiceResult> CreateAsync(PeriodoProyecto periodo)
        {
            try
            {
                _logger.Information("Creando periodo");
                periodo.Monto =await  CalcularMontoPeriodo(periodo);
                await _periodoProyectoRepository.AddAsync(periodo);
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                    MessageCode = ServiceResultMessage.Success
                };
            }
            catch (Exception ex )
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
        /// <summary>
        /// calcula el monto segun la novedad de los participantes 
        /// </summary>
        /// <param name="periodo">espera un periodo</param>
        /// <returns>retorna el monto total del periodo a facturar</returns>
        private async Task<double> CalcularMontoPeriodo(PeriodoProyecto periodo)
        {
            double tarifa = 0;
            double tarifaConvertida = 0;
            double tarifaTotal = 0;
            try
            {
                _logger.Information("Calculando monto de periodo");
                var existingProyecto = await _proyectoRepository.GetByIdAsync((int)periodo.PryId);
                if (existingProyecto!=null)
                {

                    var proyectoParticipante = await _proyectoParticipanteService.GetParticipanteByIdProAndDate(periodo);
                    foreach (var participante in proyectoParticipante)
                    {
                        int añoActual = DateTime.Now.Year;
                        var diasFeriados = await ObtenerDiasFeriados(añoActual);
                        var tarifarioConvenio=await _tarifarioConvenioRepository.GetByIdAsync((int)participante.TarifarioId);
                        var moneda = await _monedaRepository.GetByIdAsync(tarifarioConvenio.TcMoneda);
                        var novedades = await _novedadesRepository.GetAllAsync();
                        var novedadesFiltrada = novedades.Where(p => p.idProyecto == participante.PryId && p.idPersona == participante.PerId && p.IdTipoNovedad == 2);
                        if (existingProyecto.FacturacionDiaHabil==1)
                        {
                            int diasHabilesSinNovedades = CalcularDiasHabiles((DateTime) periodo.FechaPeriodoDesde, (DateTime) periodo.FechaPeriodoHasta,diasFeriados);
                            tarifa = (Double) tarifarioConvenio.TcTarifa * diasHabilesSinNovedades;
                            var result = await _monedaService.consultaMonedaConvertida("CLF",(string)moneda.MonNombre,(int) tarifa);
                            dynamic json = JObject.Parse(result);
                            tarifaConvertida = json.MonedaConvertida;
                        }
                        else
                        {
                            int diasTotalesSinNovedades = CalcularDiasTotales((DateTime) periodo.FechaPeriodoDesde, (DateTime) periodo.FechaPeriodoHasta,diasFeriados);
                            tarifa = (Double)tarifarioConvenio.TcTarifa * diasTotalesSinNovedades;
                            var result =await _monedaService.consultaMonedaConvertida("CLF",(string)moneda.MonNombre,(int) tarifa);
                            dynamic json = JObject.Parse(result);
                            tarifaConvertida = json.MonedaConvertida;
                        }
                        _logger.Information("Monto calculado de periodo");
                        tarifaTotal = tarifaTotal + tarifaConvertida;
                    }
                }
                string numeroFormateado = tarifaTotal.ToString("0.00");
                double format = double.Parse(numeroFormateado);
                return format;
            }
            catch (Exception ex)
            {
                _logger.Error("Error interno del servidor al calcular: " + ex.Message);
                return tarifa;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="diasFeriados"></param>
        /// <returns></returns>
        static int CalcularDiasTotales(DateTime startDate, DateTime endDate, List<DateTime> diasFeriados)
        {
            // Calcula la cantidad total de días, teniendo en cuenta los días feriados
            int diasTotales = 0;

            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (!EsDiaFeriado(fecha, diasFeriados))
                {
                    diasTotales++;
                }
            }

            return diasTotales;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="diasFeriados"></param>
        /// <returns></returns>
        static int CalcularDiasHabiles(DateTime startDate, DateTime endDate, List<DateTime> diasFeriados)
        {
            // Calcula la cantidad de días hábiles, sin contar feriados
            int diasHabiles = 0;

            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (EsDiaHabil(fecha) && !EsDiaFeriado(fecha, diasFeriados))
                {
                    diasHabiles++;
                }
            }

            return diasHabiles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="diasFeriados"></param>
        /// <returns></returns>
        static bool EsDiaFeriado(DateTime fecha, List<DateTime> diasFeriados)
        {
            // Verifica si la fecha es un día feriado
            return diasFeriados.Contains(fecha.Date);
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
        /// obtiene los dias feriados de la api
        /// </summary>
        /// <param name="year">espera el año</param>
        /// <returns>retorna una lista de fechas</returns>
        static async Task<List<DateTime>> ObtenerDiasFeriados(int year)
        {
            // URL del servicio web
            string url = "https://apis.digital.gob.cl/fl/feriados/"+year;

            // Realiza la solicitud HTTP y obtiene la respuesta
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    List<DateTime> feriadosfechas = new List<DateTime>();
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string contenido = await response.Content.ReadAsStringAsync();

                    // Deserializa la respuesta JSON a una lista de objetos Feriado
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
    }
}


