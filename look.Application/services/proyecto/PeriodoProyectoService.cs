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

        public PeriodoProyectoService(IPeriodoProyectoRepository periodoProyectoRepository,IUnitOfWork unitOfWork, ITarifarioConvenioRepository tarifarioConvenioService,IProyectoParticipanteRepository proyectoParticipanteService,IProyectoRepository proyectoRepository,IMonedaRepository monedaRepository,IMonedaService monedaService) : base(periodoProyectoRepository)
        {
            _periodoProyectoRepository = periodoProyectoRepository;
            _unitOfWork = unitOfWork;
            _tarifarioConvenioRepository = tarifarioConvenioService;
            _proyectoParticipanteService = proyectoParticipanteService;
            _proyectoRepository = proyectoRepository;
            _monedaRepository = monedaRepository;
            _monedaService = monedaService;
        }

        public async Task<List<PeriodoProyecto>> ListByProyecto(int id)
        {
            var periodos= await _periodoProyectoRepository.GetComplete();
            return periodos.Where(p=>p.PryId==id).ToList();
        }

        public async Task<ServiceResult> CalculateCloseBusiness(PeriodoProyecto periodoProyecto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _logger.Information("Actualizar proyecto con documentos");
                
                var existingProyecto = await _proyectoRepository.GetByIdAsync(periodoProyecto.PryId);
                if (existingProyecto!=null)
                {
                    
                    var proyectoParticipante = await _periodoProyectoRepository.GetListProyectoParticipante(periodoProyecto);
                    foreach (var participante in proyectoParticipante)
                    {
                        int añoActual = DateTime.Now.Year;
                        var diasFeriados = await ObtenerDiasFeriados(añoActual);
                        var perfilParticipante =await _tarifarioConvenioRepository.GetByIdAsync(participante.PrfId);
                        var moneda = await _monedaRepository.GetByIdAsync(perfilParticipante.TcMoneda);
                        var monedaconvertida = await _monedaService.consultaMonedaConvertida((string)moneda.MonNombre,"CLF",(int) perfilParticipante.TcTarifa);
                        
                        if (existingProyecto.FacturacionDiaHabil==1)
                        {
                            int diasHabiles = CalcularDiasHabiles((DateTime) periodoProyecto.FechaPeriodoDesde, (DateTime) periodoProyecto.FechaPeriodoHasta,diasFeriados);
                            double tarifa =((int) perfilParticipante.TcTarifa * int.Parse(monedaconvertida))* diasHabiles;
                        }
                        else
                        {
                            int diasTotales = CalcularDiasTotales((DateTime) periodoProyecto.FechaPeriodoDesde, (DateTime) periodoProyecto.FechaPeriodoHasta,diasFeriados);
                            double tarifa =((int) perfilParticipante.TcTarifa * int.Parse(monedaconvertida)) * diasTotales;
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
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error("Error al eliminar el proyecto con propuesta y documentos: " + ex.Message);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
            }
        }

        public async Task<List<PeriodoProyecto>> ListComplete()
        {
            return await _periodoProyectoRepository.GetComplete();
        }
        
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

        static bool EsDiaFeriado(DateTime fecha, List<DateTime> diasFeriados)
        {
            // Verifica si la fecha es un día feriado
            return diasFeriados.Contains(fecha.Date);
        }

        static bool EsDiaHabil(DateTime fecha)
        {
            // Verifica si el día no es sábado ni domingo
            return fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday;
        }

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

