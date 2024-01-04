using look.domain.entities.admin;

namespace look.domain.entities.proyecto
{
    public class Novedades
    {
        public int id { get; set; }

        public int? idPersona { get; set; }

        public int? idProyecto { get; set; }

        public DateTime? fechaInicio { get; set; }

        public DateTime? fechaHasta { get; set; }

        public string? observaciones { get; set; }

        public int? IdPerfil { get; set; }

        public int? IdTipoNovedad { get; set; }


        public virtual Persona? Persona { get; set; }

        public virtual Proyecto? Proyecto { get; set; }

        public virtual TipoNovedades? TipoNovedades { get; set; }

        public virtual Perfil? Perfil { get; set; }
         static public class ConstantesTipoNovedad
        {
            const int licencia = 1;
            const int vacaciones = 2;
            const int permiso = 3;
            const int cambioRol = 4;

        }
    }
}

