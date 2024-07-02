using look.domain.entities.licencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.licencia
{
    public class VentaLicenciaConfiguration : IEntityTypeConfiguration<VentaLicencia>
    {
        public void Configure(EntityTypeBuilder<VentaLicencia> builder)
        {
            builder.ToTable("venta_licencia");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            // Índices (hasIndex)
            builder.HasIndex(e => e.IdCliente, "FK_venta_licencia_cliente");
            builder.HasIndex(e => e.IdEstado, "FK_venta_licencia_estado");
            builder.HasIndex(e => e.IdMoneda, "FK_venta_licencia_moneda");
            builder.HasIndex(e => e.IdPais, "FK_venta_licencia_pais");
            builder.HasIndex(e => e.IdKam, "FK_venta_licencia_kam");
            builder.HasIndex(e => e.IdTipoFacturacion, "FK_venta_licencia_tipo_facturacion");
            builder.HasIndex(e => e.idTipoLicencia, "FK_venta_licencia_tipo_licencia");

            // Configuración de propiedades
            builder.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");

            builder.Property(e => e.Nombre)
                .HasColumnType("varchar(100)")
                .HasColumnName("nombre");

            builder.Property(e => e.FechaCierre)
                .HasColumnType("datetime")
                .HasColumnName("fecha_cierre");

            builder.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");

            builder.Property(e => e.FechaRenovacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_renovacion");

            builder.Property(e => e.IdEstado)
                .HasColumnType("int")
                .HasColumnName("id_estado");

            builder.Property(e => e.IdCliente)
                .HasColumnType("int")
                .HasColumnName("id_cliente");

            builder.Property(e => e.IdContacto)
                .HasColumnType("int")
                .HasColumnName("id_contacto");

            builder.Property(e => e.IdKam)
                .HasColumnType("int")
                .HasColumnName("id_kam");

            builder.Property(e => e.IdMoneda)
                .HasColumnType("int")
                .HasColumnName("id_moneda");

            builder.Property(e => e.Monto)
                .HasColumnType("double")
                .HasColumnName("monto");

            builder.Property(e => e.IdPais)
                .HasColumnType("int")
                .HasColumnName("id_pais");

            builder.Property(e => e.IdTipoFacturacion)
                .HasColumnType("int")
                .HasColumnName("id_tipo_facturacion");

            builder.Property(e => e.idTipoLicencia)
                .HasColumnType("int")
                .HasColumnName("id_tipo_licencia");

            //// Relaciones
            //builder.HasOne(d => d.Cliente).WithMany()
            //    .HasForeignKey(d => d.IdCliente)
            //    .HasConstraintName("FK_venta_licencia_cliente");

            //builder.HasOne(d => d.Estado).WithMany()
            //    .HasForeignKey(d => d.IdEstado)
            //    .HasConstraintName("FK_venta_licencia_estado");

            //builder.HasOne(d => d.Moneda).WithMany()
            //    .HasForeignKey(d => d.IdMoneda)
            //    .HasConstraintName("FK_venta_licencia_moneda");

            //builder.HasOne(d => d.Pais).WithMany()
            //    .HasForeignKey(d => d.IdPais)
            //    .HasConstraintName("FK_venta_licencia_pais");

            //builder.HasOne(d => d.Kam).WithMany()
            //    .HasForeignKey(d => d.IdKam)
            //    .HasConstraintName("FK_venta_licencia_kam");

            //builder.HasOne(d => d.TipoFacturacion).WithMany()
            //    .HasForeignKey(d => d.IdTipoFacturacion)
            //    .HasConstraintName("FK_venta_licencia_tipo_facturacion");

            //builder.HasOne(d => d.TipoLicencia).WithMany()
            //    .HasForeignKey(d => d.idTipoLicencia)
            //    .HasConstraintName("FK_venta_licencia_tipo_licencia");
        }
    }
}
