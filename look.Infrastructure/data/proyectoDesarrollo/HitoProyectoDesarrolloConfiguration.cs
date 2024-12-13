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
    internal class HitoProyectoDesarrolloConfiguration : IEntityTypeConfiguration<HitoProyectoDesarrollo>
    {
        public void Configure(EntityTypeBuilder<HitoProyectoDesarrollo> builder)
        {
            // Establecer el nombre de la tabla
            builder.ToTable("hito_proyecto_desarrollo");

            // Configuración de la clave primaria
            builder.HasKey(h => h.Id).HasName("PRIMARY");

            // Configuración de las propiedades
            builder.Property(h => h.Id)
                   .HasColumnType("int")
                   .HasColumnName("id");

            builder.Property(h => h.Nombre)
                   .HasColumnType("varchar(200)")
                   .HasColumnName("nombre")
                   .IsRequired(false);

            builder.Property(h => h.FechaCreacion)
                   .HasColumnType("datetime")
                   .HasColumnName("fecha_creacion")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(h => h.IdProyectoDesarrollo)
                   .HasColumnType("int")
                   .HasColumnName("id_proyecto_desarrollo")
                   .IsRequired(false);

            builder.Property(h => h.idTipoPagoHito)
                   .HasColumnType("int")
                   .HasColumnName("id_tipo_pago_hito")
                   .IsRequired(false);

            builder.Property(h => h.Monto)
                   .HasColumnType("double")
                   .HasColumnName("monto")
                   .IsRequired(false);

            builder.Property(h => h.PorcentajePagado)
                   .HasColumnType("double")
                   .HasColumnName("porcentaje_pagado")
                   .IsRequired(false);

            builder.Property(h => h.Descripcion)
                   .HasColumnType("text")
                   .HasColumnName("descripcion")
                   .IsRequired(false);

            builder.Property(h => h.HitoCumplido)
                   .HasColumnType("boolean")
                   .HasColumnName("hito_cumplido")
                   .HasDefaultValue(false);

            // Configuración de las relaciones y claves foráneas
            builder.HasOne(h => h.TipoHitoProyectoDesarrollo)
                   .WithMany()
                   .HasForeignKey(h => h.idTipoPagoHito)
                   .HasConstraintName("FK_hito_proyecto_desarrollo_tipo_hito");

            builder.HasOne(h => h.ProyectoDesarrollo)
                   .WithMany()
                   .HasForeignKey(h => h.IdProyectoDesarrollo)
                   .HasConstraintName("FK_hito_proyecto_desarrollo_proyecto_desarrollo");
        }
    }
}
