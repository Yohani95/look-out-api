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
    public class RegistroHorasProyectoDesarrolloConfiguration : IEntityTypeConfiguration<RegistroHorasProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<RegistroHorasProyectoDesarrollo> builder)
        {
            // Nombre de la tabla
            builder.ToTable("registro_horas_proyecto_desarrollo");

            // Clave primaria
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            // Configuración de propiedades
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdProfesionalProyecto)
                .HasColumnType("int(11)")
                .HasColumnName("id_profesional_proyecto");

            builder.Property(e => e.Semana)
                .HasColumnType("date")
                .HasColumnName("semana");

            builder.Property(e => e.HorasTrabajadas)
                 .HasColumnType("double") // Ahora permite decimales (ejemplo: 7.50)
                .HasColumnName("horas_trabajadas");

            // Configuración de la clave foránea
            builder.HasOne(e => e.ProfesionalProyecto)
                .WithMany()
                .HasForeignKey(e => e.IdProfesionalProyecto)
                .HasConstraintName("FK_registro_horas_proyecto_profesionales")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
