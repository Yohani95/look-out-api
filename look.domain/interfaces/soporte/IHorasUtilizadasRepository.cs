using look.domain.entities.soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.soporte
{
    public interface IHorasUtilizadasRepository:IRepository<HorasUtilizadas>
    {
        Task<List<HorasUtilizadas>> getAllHorasByIdSoporte(int id);
    }
}


