using look.domain.entities.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.world
{
    public interface IMonedaService : IService<Moneda>
    {
         Task<string>  consultaMonedaConvertida(int idTo,int idFrom,string amount);
    }
}
