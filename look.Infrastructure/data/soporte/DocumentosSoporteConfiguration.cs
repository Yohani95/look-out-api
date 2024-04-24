using look.domain.entities.soporte;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.soporte
{
    public class DocumentosSoporteConfiguration : IEntityTypeConfiguration<DocumentosSoporte>
    {
        public void Configure(EntityTypeBuilder<DocumentosSoporte> builder)
        {
            builder.ToTable("documentos_soporte");

            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.HasIndex(e => e.IdSoporte, "FK_documentos_soporte");
            builder.Property(e => e.IdSoporte)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_soporte");
            builder.Property(e => e.NombreDocumento)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre_documento");
            builder.Property(e => e.ContenidoDocumento)
                    .HasColumnType("LONGBLOB")
                    .HasColumnName("contenido_documento");
            builder.Property(e => e.idTipoDocumento)
                .HasColumnType("int(11)")
                .HasColumnName("id_tipo_documento");

               // Clave foránea a Soporte
            builder.HasOne(d => d.Soporte)
                .WithMany()
                .HasForeignKey(d => d.IdSoporte)
                .HasConstraintName("FK_documentos_soporte");

        }
    }
}
