using look.Application.interfaces.soporte;
using look.domain.entities.Common;
using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using Serilog;


namespace look.Application.services.soporte
{
    public class HorasUtilizadasService : Service<HorasUtilizadas>, IHorasUtilizadasService
    {
        private readonly IHorasUtilizadasRepository _horasUtilizadasRepository;
        private readonly ISoporteRepository _soporteRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        public HorasUtilizadasService(IHorasUtilizadasRepository repository, ISoporteRepository soporteRepository) : base(repository)
        {
            _horasUtilizadasRepository = repository;
            _soporteRepository = soporteRepository;
        }

        public async Task<List<HorasUtilizadas>> getAllHorasByIdSoporte(int id)
        {
            try
            {
                return await _horasUtilizadasRepository.getAllHorasByIdSoporte(id);
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor+ex.Message);
                throw;
            }
        }
        public new async Task<HorasUtilizadas> AddAsync(HorasUtilizadas horasUtilizadas)
        {
            try
            {
                // Obtener todas las horas utilizadas relacionadas al soporte
                var listHorasUtilizadas = await _horasUtilizadasRepository.getAllHorasByIdSoporte((int)horasUtilizadas.IdSoporte);
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);

                // Calcular las horas acumuladas utilizadas en los registros anteriores
                int horasAcumuladasUtilizadas = listHorasUtilizadas.Sum(r => r.HorasAcumuladas ?? 0);

                // Calcular las horas acumuladas restantes del registro anterior
                int horasAcumuladasRestantes = Math.Max(0, horasAcumuladasUtilizadas - ((int)soporte.NumeroHoras-(int)horasUtilizadas.Horas));

                // Calcular las horas extras para el nuevo registro
                int horasExtras = Math.Max((int)horasUtilizadas.Horas - (int) soporte.NumeroHoras, 0) - horasAcumuladasRestantes;

                // Calcular el monto de las horas extras
                double montoHorasExtras = horasExtras * (soporte.ValorHoraAdicional ?? 0);

                // Asignar los valores calculados al nuevo registro
                horasUtilizadas.HorasAcumuladas = horasAcumuladasRestantes;
                horasUtilizadas.HorasExtras = horasExtras;
                horasUtilizadas.MontoHorasExtras = montoHorasExtras;

                // Agregar el nuevo registro de horas utilizadas
                await _horasUtilizadasRepository.AddAsync(horasUtilizadas);
                return horasUtilizadas;
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }
        public new async Task<HorasUtilizadas> UpdateAsync(HorasUtilizadas horasUtilizadas)
        {
            try
            {
                // Obtener todas las horas utilizadas relacionadas al soporte
                var listHorasUtilizadas = await _horasUtilizadasRepository.getAllHorasByIdSoporte((int)horasUtilizadas.IdSoporte);
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);

                // Encontrar el registro existente que se va a actualizar
                var existingRegistro = listHorasUtilizadas.FirstOrDefault(r => r.Id == horasUtilizadas.Id);

                if (existingRegistro != null)
                {
                    // Calcular las horas acumuladas utilizadas en los registros anteriores
                    int horasAcumuladasUtilizadas = listHorasUtilizadas.Sum(r => r.HorasAcumuladas ?? 0) - (existingRegistro.HorasAcumuladas ?? 0);

                    // Calcular las horas acumuladas restantes del registro anterior
                    int horasAcumuladasRestantes = Math.Max(0, horasAcumuladasUtilizadas - ((int)soporte.NumeroHoras - (int)horasUtilizadas.Horas));

                    // Calcular las horas extras para el registro actualizado
                    int horasExtras = Math.Max((int)horasUtilizadas.Horas - (int)soporte.NumeroHoras, 0) - horasAcumuladasRestantes;

                    // Calcular el monto de las horas extras
                    double montoHorasExtras = horasExtras * (soporte.ValorHoraAdicional ?? 0);

                    // Asignar los valores calculados al registro existente
                    existingRegistro.HorasAcumuladas = horasAcumuladasRestantes;
                    existingRegistro.HorasExtras = horasExtras;
                    existingRegistro.MontoHorasExtras = montoHorasExtras;

                    // Actualizar el registro existente en la base de datos
                    await _horasUtilizadasRepository.UpdateAsync(existingRegistro);
                    return existingRegistro;
                }

                // Si no se encuentra el registro existente, lanzar una excepción o manejarlo según tu caso
                throw new Exception("Registro no encontrado");
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }

    }
}
