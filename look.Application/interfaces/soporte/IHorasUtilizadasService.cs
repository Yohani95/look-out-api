﻿using look.domain.entities.soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.soporte
{
    public interface IHorasUtilizadasService:IService<HorasUtilizadas>
    {
        Task<List<HorasUtilizadas>> getAllHorasByIdSoporte(int id);
        Task<HorasUtilizadas> CreateBag(HorasUtilizadas horasUtilizadas);
        Task UpdateBag(HorasUtilizadas horasUtilizadas, int id);
        Task<HorasUtilizadas> CreateOnDemand(HorasUtilizadas horasUtilizadas);
        Task UpdateOnDemand(HorasUtilizadas horasUtilizadas, int id);
    }
}
