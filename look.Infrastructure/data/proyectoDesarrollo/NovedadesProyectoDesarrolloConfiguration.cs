using look.domain.entities.proyectoDesarrollo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.proyectoDesarrollo
{
    internal class NovedadesProyectoDesarrolloConfiguration : IEntityTypeConfiguration<NovedadesProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<NovedadesProyectoDesarrollo> builder)
        {
            // Establecer el nombre de la tabla
            builder.ToTable("novedades_proyecto_desarrollo");

            // Configuración de la clave primaria
            builder.HasKey(n => n.Id).HasName("PRIMARY");

            // Configuración de las propiedades
            builder.Property(n => n.Id)
                   .HasColumnType("int(11)")
                   .HasColumnName("id");

            builder.Property(n => n.nombre)
                   .HasColumnType("varchar(200)")
                   .HasColumnName("nombre")
                   .IsRequired(false);

            builder.Property(n => n.FechaCreacion)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_creacion")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .IsRequired();

            builder.Property(n => n.IdProyectoDesarrollo)
                   .HasColumnType("int")
                   .HasColumnName("id_proyecto_desarrollo")
                   .IsRequired(false);

            builder.Property(n => n.IdTipoNovedadProyectoDesarrollo)
                   .HasColumnType("int")
                   .HasColumnName("id_tipo_novedad_proyecto_desarrollo")
                   .IsRequired(false);

            builder.Property(n => n.Descripcion)
                   .HasColumnType("text")
                   .HasColumnName("descripcion")
                   .IsRequired(false);

            builder.Property(n => n.IdKam)
                   .HasColumnType("int")
                   .HasColumnName("id_kam")
                   .IsRequired(false);

            // Configuración de las relaciones y claves foráneas
            builder.HasOne(n => n.TipoNovedad)
                   .WithMany()
                   .HasForeignKey(n => n.IdTipoNovedadProyectoDesarrollo)
                   .HasConstraintName("FK_novedades_proyecto_desarrollo_tipo_novedad");

            builder.HasOne(n => n.Persona)
                   .WithMany()
                   .HasForeignKey(n => n.IdKam)
                   .HasConstraintName("FK_novedades_proyecto_desarrollo_kam");

            builder.HasOne<ProyectoDesarrollo>()
                   .WithMany()
                   .HasForeignKey(n => n.IdProyectoDesarrollo)
                   .HasConstraintName("FK_novedades_proyecto_desarrollo_proyecto_desarrollo");
        }
    }
}
