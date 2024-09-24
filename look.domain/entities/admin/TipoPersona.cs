using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.admin
{
    public class TipoPersona
    {
        public int Id { get; set; }
        public string TpeDescripcion { get; set; }

        // Constantes como propiedades estáticas
        public static readonly TipoPersona Admin = new TipoPersona { Id = 1, TpeDescripcion = "admin" };
        public static readonly TipoPersona Kam = new TipoPersona { Id = 2, TpeDescripcion = "kam" };
        public static readonly TipoPersona ContactoCuenta = new TipoPersona { Id = 3, TpeDescripcion = "contacto cuenta" };
        public static readonly TipoPersona Profesional = new TipoPersona { Id = 4, TpeDescripcion = "Profesional" };
    }
}
