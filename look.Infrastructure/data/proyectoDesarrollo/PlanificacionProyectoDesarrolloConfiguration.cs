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
    internal class PlanificacionProyectoDesarrolloConfiguration : IEntityTypeConfiguration<PlanificacionProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<PlanificacionProyectoDesarrollo> builder)
        {
            // Establecer el nombre de la tabla
            builder.ToTable("planificacion_proyecto_desarrollo");

            // Configuración de la clave primaria
            builder.HasKey(p => p.Id).HasName("PRIMARY");

            // Configuración de las propiedades
            builder.Property(p => p.Id)
                   .HasColumnType("int(11)")
                   .HasColumnName("id");

            builder.Property(p => p.Nombre)
                   .HasColumnType("varchar(255)")
                   .HasColumnName("nombre")
                   .IsRequired(false);

            builder.Property(p => p.PorcentajeCargaTrabajo)
                   .HasColumnType("double")
                   .HasColumnName("porcentaje_carga_trabajo")
                   .IsRequired(false);

            builder.Property(p => p.IdEtapa)
                   .HasColumnType("int")
                   .HasColumnName("id_etapa")
                   .IsRequired(false);

            builder.Property(p => p.LineaBase)
                   .HasColumnType("tinyint(1)")
                   .HasColumnName("linea_base")
                   .IsRequired(false);

            builder.Property(p => p.IdProyectoDesarrollo)
                   .HasColumnType("int")
                   .HasColumnName("id_proyecto_desarrollo");

            builder.Property(n => n.FechaCreacion)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_creacion")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .IsRequired(false);

            builder.Property(n => n.FechaActividad)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_actividad")
                   .IsRequired(false);

            builder.Property(n => n.FechaInicio)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_inicio")
                   .IsRequired(false);

            builder.Property(n => n.FechaTermino)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_termino")
                   .IsRequired(false);

            builder.Property(n => n.FechaTerminoReal)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_termino_real")
                   .IsRequired(false);

            builder.Property(p => p.Terminado)
                   .HasColumnType("tinyint(1)")
                   .HasColumnName("Terminado")
                   .IsRequired(false);
            // Configuración de la relación y clave foránea
            builder.HasOne(p => p.Etapa)
                   .WithMany()
                   .HasForeignKey(p => p.IdEtapa)
                   .HasConstraintName("FK_planificacion_proyecto_desarrollo_etapa");
        }
    }
}

