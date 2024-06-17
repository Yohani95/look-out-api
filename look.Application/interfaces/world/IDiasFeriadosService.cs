using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.world
{
    public interface IDiasFeriadosService:IService<DiasFeriados>
    {
        public Task<List<DiasFeriados>> ConsultarYGuardarFeriados(string country, int year,int idPais);
    }
}
