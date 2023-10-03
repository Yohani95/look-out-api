namespace look.domain.entities.proyecto
{
    public class ProyectoDocumento
    {
        public int PydId { get; set; }
        
        public int PryId { get; set; }
        
        public int DocId { get; set; }
        
        public int TdoId { get; set; }
        
        public virtual Documento? Documento { get; set; }
        
        public virtual Proyecto? Proyecto { get; set; }
        
        public virtual TipoDocumento? TipoDocumento { get; set; }
    }
}