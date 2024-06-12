using look.domain.entities.oportunidad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace look.Infrastructure.data.oportunidad
{
    public class DocumentoOportunidadConfiguration : IEntityTypeConfiguration<DocumentoOportunidad>
    {
        public void Configure(EntityTypeBuilder<DocumentoOportunidad> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("documento_oportunidad");
            entity.HasIndex(e => e.IdOportunidad, "FK_documento_oportunidad");
            entity.Property(e => e.NombreDocumento)
                .HasColumnType("varchar(255)")
                .HasColumnName("nombre_documento");
            entity.Property(e => e.ContenidoDocumento)
                .HasColumnType("LONGBLOB")
                .HasColumnName("contenido_documento");
            entity.Property(e => e.IdOportunidad)
                .HasColumnType("int(11)")
                .HasColumnName("id_oportunidad");
            entity.Property(e => e.Descripcion)
                .HasColumnType("varchar(255)");
            entity.HasOne(d => d.Oportunidad)
                .WithMany(d => d.DocumentosOportunidad)
                .HasForeignKey(d => d.IdOportunidad)
                .HasConstraintName("FK_documento_oportunidad");
        }
    }
}
