﻿using look.domain.dto.proyecto;
using look.domain.entities.admin;
using look.domain.entities.Common;
using look.domain.entities.factura;
using look.domain.entities.oportunidad;
using look.domain.entities.proyecto;

namespace look.Application.interfaces.admin
{
    public interface IEmailService : IService<Email>
    {
        Task<List<Email>> ListComplete();
        /// <summary>
        /// obtiene email segun el id de personas
        /// </summary>
        /// <param name="id">id de persona</param>
        /// <returns>retorna una lista de email</returns>
        Task<List<Email>> ListCompleteById(int id);
        Task<ServiceResult> Create(Email email);
        Task<ServiceResult> Edit(Email email, int id);
        /// <summary>
        /// envia un email 
        /// </summary>
        /// <param name="to">para</param>
        /// <param name="subject">asunto</param>
        /// <param name="body">cuerpo</param>
        /// <returns></returns>
        Task SendEmailAsync(string toName, string toEmail, string subject, string body, bool copiaFija = true);
        /// <summary>
        /// envia un email  a responsable delevery
        /// </summary>
        /// <param name="oportunidad">entidad oportunidad</param>
        /// <returns></returns>
        Task EnviarEmailDelevery(Oportunidad oportunidad);

        Task EnviarEmailKam(int id, Oportunidad oportunidad);

        Task EnviarEmailSuporvisorFacturas(PeriodoProyectoDto periodoProyecto);
    }
}