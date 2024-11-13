using look.domain.entities.oportunidad;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using look.domain.entities.proyectoDesarrollo;

namespace look.Infrastructure.data.proyectoDesarrollo
{
    public class DocumentoProyectoDesarrolloConfiguration : IEntityTypeConfiguration<DocumentoProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<DocumentoProyectoDesarrollo> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("documento_proyecto_desarrollo");
            entity.HasIndex(e => e.IdProyectoDesarrollo, "FK_documento_proyecto_desarrollo");
            entity.Property(e => e.NombreDocumento)
                .HasColumnType("varchar(255)")
                .HasColumnName("nombre_documento");
            entity.Property(e => e.ContenidoDocumento)
                .HasColumnType("LONGBLOB")
                .HasColumnName("contenido_documento");
            entity.Property(e => e.IdProyectoDesarrollo)
                .HasColumnType("int(11)")
                .HasColumnName("id_proyecto_desarrollo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("varchar(255)");
        }
    }
}
