namespace look.domain.entities.proyecto
{
    public class Resultado
    {
        public string Version { get; set; }
        public string Autor { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }
        public SerieItem[] Serie { get; set; }
    }
}
