using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.oportunidad
{
    public class EstadoOportunidad
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }    
        public string? Descripcion { get; set; }
        // Constantes predefinidas
        public static readonly EstadoOportunidad Calificacion = new EstadoOportunidad
        {
            Id = 1,
            Nombre = "Estado 1",
            Descripcion = "Calificación"
        };

        public static readonly EstadoOportunidad ReunionProgramada = new EstadoOportunidad
        {
            Id = 2,
            Nombre = "Estado 2",
            Descripcion = "Reunión Programada"
        };

        public static readonly EstadoOportunidad PropuestaEnPreparacion = new EstadoOportunidad
        {
            Id = 3,
            Nombre = "Estado 3",
            Descripcion = "Propuesta en Preparación"
        };

        public static readonly EstadoOportunidad PropuestaEntregadaComercial = new EstadoOportunidad
        {
            Id = 4,
            Nombre = "Estado 4",
            Descripcion = "Propuesta Entregada a Comercial"
        };

        public static readonly EstadoOportunidad PropuestaEnviadaCliente = new EstadoOportunidad
        {
            Id = 5,
            Nombre = "Estado 5",
            Descripcion = "Propuesta Enviada a Cliente"
        };

        public static readonly EstadoOportunidad Comprometido = new EstadoOportunidad
        {
            Id = 6,
            Nombre = "Estado 6",
            Descripcion = "Comprometido"
        };

        public static readonly EstadoOportunidad CerradaPerdida = new EstadoOportunidad
        {
            Id = 7,
            Nombre = "Estado 7",
            Descripcion = "Cerrada Perdida"
        };

        public static readonly EstadoOportunidad CerradaGanada = new EstadoOportunidad
        {
            Id = 8,
            Nombre = "Estado 8",
            Descripcion = "Cerrada Ganada"
        };
    }
}
