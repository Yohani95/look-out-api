using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace look.domain.entities.admin
{
    public class RolFuncionalidad
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int FuncionalidadId { get; set; }
        public bool TieneAcceso { get; set; }


        [JsonIgnore] // Evita la serialización de la propiedad `Rol`
        public virtual Rol Rol { get; set; }
    }
}
