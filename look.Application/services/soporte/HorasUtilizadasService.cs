﻿using look.Application.interfaces.soporte;
using look.domain.entities.Common;
using look.domain.entities.soporte;
using look.domain.interfaces.soporte;
using look.domain.interfaces.unitOfWork;
using Microsoft.Win32;
using Serilog;
using System.Reflection.Metadata;

namespace look.Application.services.soporte
{
    public class HorasUtilizadasService : Service<HorasUtilizadas>, IHorasUtilizadasService
    {
        private readonly IHorasUtilizadasRepository _horasUtilizadasRepository;
        private readonly ISoporteRepository _soporteRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        public HorasUtilizadasService(IHorasUtilizadasRepository repository, ISoporteRepository soporteRepository, IUnitOfWork unitOfWork) : base(repository)
        {
            _horasUtilizadasRepository = repository;
            _soporteRepository = soporteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HorasUtilizadas>> getAllHorasByIdSoporte(int id)
        {
            try
            {
                return await _horasUtilizadasRepository.getAllHorasByIdSoporte(id);
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }
        public new async Task<HorasUtilizadas> AddAsync(HorasUtilizadas horasUtilizadas)
        {
            try
            {
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);

                // Obtener todas las horas utilizadas relacionadas al soporte
                var listHorasUtilizadas = await _horasUtilizadasRepository.getAllHorasByIdSoporte((int)horasUtilizadas.IdSoporte);

                // Asegurar valores predeterminados en caso de valores nulos
                double horasUtilizadasHoras = horasUtilizadas.Horas ?? 0;
                double soporteNumeroHoras = soporte.NumeroHoras ?? 0;
                double ultimoRegistroHorasAcumuladas = listHorasUtilizadas.LastOrDefault()?.HorasAcumuladas ?? 0;

                double horaAcumulada = Math.Max(soporteNumeroHoras - horasUtilizadasHoras, 0);

                if (listHorasUtilizadas.Count != 0)
                {
                    horasUtilizadas.HorasExtras = Math.Max(horasUtilizadasHoras - soporteNumeroHoras - ultimoRegistroHorasAcumuladas, 0);

                    horasUtilizadas.MontoHorasExtras = horasUtilizadas.HorasExtras * (soporte.ValorHoraAdicional ?? 0);

                    horasUtilizadas.Monto = soporte.PryValor ?? 0;

                    double horaDescuento = Math.Max(horasUtilizadasHoras - soporteNumeroHoras, 0);

                    horasUtilizadas.HorasAcumuladas = (soporte.AcumularHoras ?? false)
                        ? Math.Max(horaAcumulada + ultimoRegistroHorasAcumuladas - horaDescuento, 0)
                        : 0;
                }
                else
                {
                    horasUtilizadas.HorasExtras = Math.Max(horasUtilizadasHoras - soporteNumeroHoras, 0);

                    horasUtilizadas.MontoHorasExtras = horasUtilizadas.HorasExtras * (soporte.ValorHoraAdicional ?? 0);

                    horasUtilizadas.Monto = horasUtilizadas.MontoHorasExtras + (soporte.PryValor ?? 0);

                    horasUtilizadas.HorasAcumuladas = (soporte.AcumularHoras ?? false) ? horaAcumulada : 0;
                }

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

        public async Task UpdateAsync(HorasUtilizadas horasUtilizadas)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Obtener el soporte relacionado a las horas utilizadas
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);
                // Obtener todas las horas utilizadas relacionadas al soporte
                var listHorasUtilizadas = await _horasUtilizadasRepository.getAllHorasByIdSoporte((int)horasUtilizadas.IdSoporte);
                var horaAcumulada = Math.Max((double)soporte.NumeroHoras - (double)horasUtilizadas.Horas, 0);

                // Buscar el índice del registro que se está modificando
                var index = listHorasUtilizadas.FindIndex(h => h.Id == horasUtilizadas.Id);
                horasUtilizadas.HorasExtras = Math.Max((double)horasUtilizadas.Horas - (double)soporte.NumeroHoras, 0);
                if (index > 0)
                {
                    var registroAnterior = listHorasUtilizadas[index - 1];
                    horasUtilizadas.HorasExtras = Math.Max((double)horasUtilizadas.Horas - (double)soporte.NumeroHoras - (double)registroAnterior.HorasAcumuladas, 0);
                    var horaDescuento = Math.Max(((double)horasUtilizadas.Horas - (double)soporte.NumeroHoras), 0);
                    horaAcumulada = Math.Max((horaAcumulada + (double)registroAnterior.HorasAcumuladas) - horaDescuento, 0);
                }
                horasUtilizadas.MontoHorasExtras = horasUtilizadas.HorasExtras * soporte.ValorHoraAdicional;
                horasUtilizadas.Monto = soporte.PryValor;
                horasUtilizadas.HorasAcumuladas = (soporte.AcumularHoras ?? false) ? horaAcumulada : 0;
                // Actualizar el registro seleccionado
                listHorasUtilizadas[index].Horas = horasUtilizadas.Horas;
                listHorasUtilizadas[index].HorasExtras = horasUtilizadas.HorasExtras;
                listHorasUtilizadas[index].HorasAcumuladas = horasUtilizadas.HorasAcumuladas;
                listHorasUtilizadas[index].MontoHorasExtras = horasUtilizadas.MontoHorasExtras;
                listHorasUtilizadas[index].Monto = horasUtilizadas.Monto;
                listHorasUtilizadas[index].NombreDocumento = horasUtilizadas.NombreDocumento;
                listHorasUtilizadas[index].ContenidoDocumento = horasUtilizadas.ContenidoDocumento;
                await _horasUtilizadasRepository.UpdateAsync(listHorasUtilizadas[index]);
                // Actualizar los registros siguientes en cascada
                for (int i = index + 1; i < listHorasUtilizadas.Count; i++)
                {
                    var registro = listHorasUtilizadas[i];
                    horaAcumulada = Math.Max((double)soporte.NumeroHoras - (double)registro.Horas, 0);
                    var horaAcumuladaUtilizada = (double)listHorasUtilizadas[i - 1].HorasAcumuladas;
                    registro.HorasExtras = Math.Max(((double)registro.Horas - (double)soporte.NumeroHoras) - (double)listHorasUtilizadas[i - 1].HorasAcumuladas, 0);
                    registro.MontoHorasExtras = registro.HorasExtras * soporte.ValorHoraAdicional;
                    registro.Monto = soporte.PryValor;
                    if (registro.Horas > soporte.NumeroHoras)
                    {
                        var horaDescuento = Math.Max(((double)registro.Horas - (double)soporte.NumeroHoras), 0);
                        horaAcumuladaUtilizada = Math.Max(horaAcumuladaUtilizada - horaDescuento, 0);
                    }
                    registro.HorasAcumuladas = (soporte.AcumularHoras ?? false) ? horaAcumulada + horaAcumuladaUtilizada : 0;
                    listHorasUtilizadas[i] = registro;
                    // Actualizar el registro en la base de datos
                    await _horasUtilizadasRepository.UpdateAsync(registro);
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }

        public async Task<HorasUtilizadas> CreateBag(HorasUtilizadas horasUtilizadas)
        {
            try
            {
                // Obtener el soporte relacionado a las horas utilizadas
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);
                // Obtener todas las horas utilizadas relacionadas al soporte
                var listHorasUtilizadas = await _horasUtilizadasRepository.getAllHorasByIdSoporte((int)horasUtilizadas.IdSoporte);
                horasUtilizadas.Monto = 0;
                if (listHorasUtilizadas.Count != 0)
                {
                    var ultimoRegistro = listHorasUtilizadas.Last();
                    horasUtilizadas.HorasAcumuladas = ultimoRegistro.HorasAcumuladas - horasUtilizadas.Horas;
                }
                else
                {
                    horasUtilizadas.HorasAcumuladas = (double)soporte.NumeroHoras - horasUtilizadas.Horas;
                }
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

        public async Task UpdateBag(HorasUtilizadas horasUtilizadas, int id)
        {
            try
            {
                // Obtener el soporte relacionado a las horas utilizadas
                //var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);
                //// Obtener todas las horas utilizadas relacionadas al soporte
                //var horasUtilizadasExistente = await _horasUtilizadasRepository.GetByIdAsync(id);
                //horasUtilizadas.Monto = soporte.PryValor;
                //// Actualizar el registro seleccionado
                //horasUtilizadasExistente.Horas = horasUtilizadas.Horas;
                //horasUtilizadasExistente.HorasExtras = horasUtilizadas.HorasExtras;
                //horasUtilizadasExistente.HorasAcumuladas = horasUtilizadas.HorasAcumuladas;
                //horasUtilizadasExistente.MontoHorasExtras = horasUtilizadas.MontoHorasExtras;
                //horasUtilizadasExistente.Monto = horasUtilizadas.Monto;
                //horasUtilizadasExistente.NombreDocumento = horasUtilizadas.NombreDocumento;
                //horasUtilizadasExistente.ContenidoDocumento = horasUtilizadas.ContenidoDocumento;
                //await _horasUtilizadasRepository.UpdateAsync(horasUtilizadasExistente);



                await _unitOfWork.BeginTransactionAsync();
                // Obtener el soporte relacionado a las horas utilizadas
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);
                // Obtener todas las horas utilizadas relacionadas al soporte
                var listHorasUtilizadas = await _horasUtilizadasRepository.getAllHorasByIdSoporte((int)horasUtilizadas.IdSoporte);
                var horaAcumulada = Math.Max((double)soporte.NumeroHoras - (double)horasUtilizadas.Horas, 0);

                // Buscar el índice del registro que se está modificando
                var index = listHorasUtilizadas.FindIndex(h => h.Id == horasUtilizadas.Id);
                if (index > 0)
                {
                    var registroAnterior = listHorasUtilizadas[index - 1];
                    horaAcumulada = (double)registroAnterior.HorasAcumuladas - (double)horasUtilizadas.Horas;
                }
                horasUtilizadas.Monto = 0;
                horasUtilizadas.HorasAcumuladas = horaAcumulada;
                // Actualizar el registro seleccionado
                listHorasUtilizadas[index].Horas = horasUtilizadas.Horas;
                listHorasUtilizadas[index].HorasExtras = horasUtilizadas.HorasExtras;
                listHorasUtilizadas[index].HorasAcumuladas = horasUtilizadas.HorasAcumuladas;
                listHorasUtilizadas[index].MontoHorasExtras = horasUtilizadas.MontoHorasExtras;
                listHorasUtilizadas[index].Monto = horasUtilizadas.Monto;
                listHorasUtilizadas[index].NombreDocumento = horasUtilizadas.NombreDocumento;
                listHorasUtilizadas[index].ContenidoDocumento = horasUtilizadas.ContenidoDocumento;
                await _horasUtilizadasRepository.UpdateAsync(listHorasUtilizadas[index]);
                // Actualizar los registros siguientes en cascada
                for (int i = index + 1; i < listHorasUtilizadas.Count; i++)
                {
                    var registro = listHorasUtilizadas[i];
                    horaAcumulada = (double)listHorasUtilizadas[i - 1].HorasAcumuladas - (double)registro.Horas;
                    registro.HorasAcumuladas = horaAcumulada;
                    listHorasUtilizadas[i] = registro;
                    // Actualizar el registro en la base de datos
                    await _horasUtilizadasRepository.UpdateAsync(registro);
                }
                await _unitOfWork.CommitAsync();


            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }

        public async Task<HorasUtilizadas> CreateOnDemand(HorasUtilizadas horasUtilizadas)
        {
            try
            {
                // Obtener el soporte relacionado a las horas utilizadas
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);
                // Obtener todas las horas utilizadas relacionadas al soporte
                horasUtilizadas.Monto = horasUtilizadas.Horas * soporte.PryValor;
                await _horasUtilizadasRepository.AddAsync(horasUtilizadas);
                return horasUtilizadas;
            }
            catch (Exception ex)
            {
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }

        public async Task UpdateOnDemand(HorasUtilizadas horasUtilizadas, int id)
        {
            try
            {
                // Obtener el soporte relacionado a las horas utilizadas
                var soporte = await _soporteRepository.GetByIdAsync((int)horasUtilizadas.IdSoporte);
                // Obtener todas las horas utilizadas relacionadas al soporte
                var horasUtilizadasExistente = await _horasUtilizadasRepository.GetByIdAsync(id);
                horasUtilizadas.Monto = soporte.PryValor * horasUtilizadas.Horas;
                // Actualizar el registro seleccionado
                horasUtilizadasExistente.Horas = horasUtilizadas.Horas;
                horasUtilizadasExistente.HorasExtras = horasUtilizadas.HorasExtras;
                horasUtilizadasExistente.HorasAcumuladas = horasUtilizadas.HorasAcumuladas;
                horasUtilizadasExistente.MontoHorasExtras = horasUtilizadas.MontoHorasExtras;
                horasUtilizadasExistente.Monto = horasUtilizadas.Monto;
                horasUtilizadasExistente.NombreDocumento = horasUtilizadas.NombreDocumento;
                horasUtilizadasExistente.ContenidoDocumento = horasUtilizadas.ContenidoDocumento;
                await _horasUtilizadasRepository.UpdateAsync(horasUtilizadasExistente);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.Error(Message.ErrorServidor + ex.Message);
                throw;
            }
        }
    }
}
