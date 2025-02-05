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
    public class ProfesionalProyectoDesarrolloConfiguration : IEntityTypeConfiguration<ProfesionalesProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<ProfesionalesProyectoDesarrollo> builder)
        {
            builder.ToTable("profesionales_proyecto_desarrollo");


            // Configuración de la clave primaria
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            // Configuración de propiedades
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");

            builder.Property(e => e.FechaTermino)
                .HasColumnType("datetime")
                .HasColumnName("fecha_termino");

            builder.Property(e => e.IdPersona)
                .HasColumnType("int(11)")
                .HasColumnName("id_persona");

            builder.Property(e => e.IdProyectoDesarrollo)
                .HasColumnType("int(11)")
                .HasColumnName("id_proyecto_desarrollo");

            // Configuración de relaciones
            builder.HasOne(e => e.Persona)
                .WithMany() // Asume que `Persona` no tiene una colección de `ProfesionalesProyectoDesarrollo`
                .HasForeignKey(e => e.IdPersona)
                .HasConstraintName("FK_profesionales_proyecto_desarrollo_persona");

        }
    }
}
