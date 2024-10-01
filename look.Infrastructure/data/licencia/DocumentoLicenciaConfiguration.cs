using look.domain.entities.licencia;
using look.domain.entities.proyecto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.licencia
{
    internal class DocumentoLicenciaConfiguration : IEntityTypeConfiguration<DocumentoLicencia>
    {
        public void Configure(EntityTypeBuilder<DocumentoLicencia> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("documento_licencia");
            entity.HasIndex(e => e.IdLicencia, "FK_documento_licencia");
            entity.Property(e => e.NombreDocumento)
                .HasColumnType("varchar(255)")
                .HasColumnName("nombre_documento");
            entity.Property(e => e.ContenidoDocumento)
                .HasColumnType("LONGBLOB")
                .HasColumnName("contenido_documento");
            entity.Property(e => e.IdLicencia)
                .HasColumnType("int(11)")
                .HasColumnName("id_licencia");
            entity.Property(e => e.Descripcion)
                .HasColumnType("varchar(255)");
            entity.HasOne(d => d.VentaLicencia)
                .WithMany(d => d.DocumentoLicencia)
                .HasForeignKey(d => d.IdLicencia)
                .HasConstraintName("FK_documento_licencia");
        }
    }
}
