using look.domain.entities.oportunidad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.oportunidad
{
    public class OportunidadConfiguration : IEntityTypeConfiguration<Oportunidad>
    {
        public void Configure(EntityTypeBuilder<Oportunidad> builder)
        {
            builder.ToTable("oportunidad");

            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.HasIndex(e => e.IdCliente, "FK_oportunidad_cliente");
            builder.HasIndex(e => e.IdEstadoOportunidad, "FK_oportunidad_Estado_oportunidad");
            builder.HasIndex(e => e.IdMoneda, "FK_oportunidad_Moneda");
            builder.HasIndex(e => e.IdEmpresaPrestadora, "FK_oportunidad_Empresa_Prestadora");
            builder.HasIndex(e => e.IdTipoOportunidad, "FK_oportunidad_tipo_oportunidad");
            builder.HasIndex(e => e.IdPais, "FK_oportunidad_Pais");
            builder.HasIndex(e => e.IdAreaServicio, "FK_oportunidad_area_servicio");
            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            builder.Property(e => e.Nombre)
                .HasColumnType("varchar(100)")
                .HasColumnName("nombre");
            builder.Property(e => e.FechaCierre)
                .HasColumnType("datetime")
                .HasColumnName("fecha_cierre");
            builder.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("id_cuenta");
            builder.Property(e => e.IdMoneda)
                .HasColumnType("int(11)")
                .HasColumnName("id_moneda");
            builder.Property(e => e.Monto)
                .HasColumnName("monto")
                .HasColumnType("double");
            builder.Property(e => e.IdTipoOportunidad)
                .HasColumnType("int(11)")
                .HasColumnName("id_tipo_oportunidad");
            builder.Property(e => e.Renovable)
                .HasColumnName("renovable")
                .HasColumnType("boolean")
                .HasDefaultValue(false);
            builder.Property(e => e.Licitacion)
                .HasColumnName("licitacion")
                .HasColumnType("licitacion")
                .HasDefaultValue(false);
            builder.Property(e => e.IdEmpresaPrestadora)
                .HasColumnType("int(11)")
                .HasColumnName("id_empresa_prestadora");
            builder.Property(e => e.FechaRenovacion)
               .HasColumnType("datetime")
               .HasColumnName("fecha_renovacion");
            builder.Property(e => e.IdPais)
                .HasColumnType("int(11)")
                .HasColumnName("id_pais");
            builder.Property(e => e.IdEstadoOportunidad)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado_oportunidad");
            builder.Property(e => e.IdAreaServicio)
                .HasColumnType("int(11)")
                .HasColumnName("id_area_servicio");
            builder.Property(e => e.IdContacto)
                .HasColumnType("int(11)")
                .HasColumnName("id_contacto");

            builder.HasOne(d => d.Cliente).WithMany()
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_oportunidad_Cliente");
            builder.HasOne(d => d.TipoOportunidad).WithMany()
                .HasForeignKey(d => d.IdTipoOportunidad)
                .HasConstraintName("FK_oportunidad_tipo_oportunida");
            builder.HasOne(d => d.Pais).WithMany()
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_oportunidad_Pais");
            builder.HasOne(d => d.EmpresaPrestadora).WithMany()
                .HasForeignKey(d => d.IdEmpresaPrestadora)
                .HasConstraintName("FK_oportunidad_Empresa_Prestadora");
            builder.HasMany(d => d.DocumentosOportunidad)
                .WithOne(d => d.Oportunidad)
                .HasForeignKey(d => d.IdOportunidad)
                .HasConstraintName("FK_documento_oportunidad");
            builder.HasOne(d => d.Moneda).WithMany()
                .HasForeignKey(d => d.IdMoneda)
                .HasConstraintName("FK_oportunidad_Moneda");
            builder.HasOne(d => d.EstadoOportunidad).WithMany()
                .HasForeignKey(d => d.IdEstadoOportunidad)
                .HasConstraintName("FK_Estado_Oportunidad");
        }
    }
}
