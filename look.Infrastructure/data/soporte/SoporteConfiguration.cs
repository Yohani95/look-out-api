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
    public class SoporteConfiguration : IEntityTypeConfiguration<Soporte>
    {
        public void Configure(EntityTypeBuilder<Soporte> builder)
        {

            builder.HasKey(e => e.PryId).HasName("PRIMARY");
            builder.ToTable("soporte");
            builder.HasIndex(e => e.PryIdCliente, "FK_Proyecto_Cliente");
            builder.HasIndex(e => e.EpyId, "FK_Proyecto_Estado_Proyecto");
            builder.HasIndex(e => e.MonId, "FK_Proyecto_Moneda");
            builder.HasIndex(e => e.PrpId, "FK_Proyecto_Propuesta");
            builder.HasIndex(e => e.TseId, "FK_Proyecto_Tipo_Servicio");
            builder.HasIndex(e => e.PaisId, "FK_Proyecto_Pais");
            builder.HasIndex(e => e.IdDiaPago, "FK_Proyecto_Dia_Pago");
            builder.HasIndex(e => e.IdTipoSoporte, "FK_soporte_tipo_soporte");

            builder.HasIndex(e => e.idEmpresaPrestadora, "FK_Proyecto_Empresa_Prestadora");
            builder.Property(e => e.PryId)
                .HasColumnType("int(11)")
                .HasColumnName("pry_id");
            builder.Property(e => e.PryNombre)
                .HasColumnType("varchar(50)")
                .HasColumnName("pry_nombre");
            builder.Property(e => e.PrpId)
                .HasColumnType("int(11)")
                .HasColumnName("prp_id");
            builder.Property(e => e.EpyId)
                .HasColumnType("int(11)")
                .HasColumnName("epy_id");
            builder.Property(e => e.TseId)
                .HasColumnType("int(11)")
                .HasColumnName("tse_id");
            builder.Property(e => e.PryFechaInicioEstimada)
                .HasColumnType("datetime")
                .HasColumnName("pry_fecha_inicio_estimada");
            builder.Property(e => e.PryValor)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("pry_valor");
            builder.Property(e => e.MonId)
               .HasColumnType("int(11)")
               .HasColumnName("mon_id");
            builder.Property(e => e.PryIdCliente)
               .HasColumnType("int(11)")
               .HasColumnName("pry_id_cliente");
            builder.Property(e => e.PryFechaCierreEstimada)
               .HasColumnType("datetime")
               .HasColumnName("pry_fecha_cierre_estimada");
            builder.Property(e => e.PryFechaCierre)
             .HasColumnType("datetime")
             .HasColumnName("pry_fecha_cierre");
            builder.Property(e => e.PryIdContacto)
              .HasColumnType("int(11)")
              .HasColumnName("pry_id_contacto");
            builder.Property(e => e.kamId)
             .HasColumnType("int(11)")
             .HasColumnName("pry_kamId");
            builder.Property(e => e.PaisId)
             .HasColumnType("int(11)")
             .HasColumnName("pai_id");
            builder.Property(e => e.FechaCorte)
                .HasColumnType("int")
                .HasColumnName("fecha_corte");
            builder.Property(e => e.FacturacionDiaHabil)
              .HasColumnType("tinyint(2)")
              .HasColumnName("facturacion_dia_habil");

            builder.Property(e => e.idTipoFacturacion)
              .HasColumnType("int(11)")
              .HasColumnName("id_tipo_factura");

            builder.Property(e => e.IdDiaPago)
            .HasColumnType("int(11)")
            .HasColumnName("id_dia_pago");

            builder.Property(e => e.idEmpresaPrestadora)
            .HasColumnType("int(11)")
            .HasColumnName("id_empresa_prestadora");
            builder.Property(e => e.ValorHoraAdicional).HasColumnName("valor_hora_adicional").HasColumnType("double");
            builder.Property(e => e.ValorHora).HasColumnName("valor_hora");
            builder.Property(e => e.AcumularHoras).HasColumnName("acumular_horas").HasColumnType("boolean").HasDefaultValue(false);
            builder.Property(e => e.NumeroHoras).HasColumnName("numero_horas").HasColumnType("double");
            builder.Property(e => e.IdTipoSoporte).HasColumnName("id_tipo_soporte").HasColumnType("int");

            builder.HasOne(d => d.Cliente).WithMany()
                .HasForeignKey(d => d.PryIdCliente)
                .HasConstraintName("FK_Proyecto_Cliente");
            builder.HasOne(d => d.EsProy).WithMany()
                .HasForeignKey(d => d.EpyId)
                .HasConstraintName("FK_Proyecto_Estado_Proyecto");
            builder.HasOne(d => d.Mon).WithMany()
                .HasForeignKey(d => d.MonId)
                .HasConstraintName("FK_Proyecto_Moneda");
            builder.HasOne(d => d.TipoServicio).WithMany()
              .HasForeignKey(d => d.TseId)
              .HasConstraintName("FK_Proyecto_Tipo_Servicio");
            builder.HasOne(d => d.Pais).WithMany()
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK_Proyecto_Pais");
            builder.HasOne(d => d.EmpresaPrestadora).WithMany()
               .HasForeignKey(d => d.idEmpresaPrestadora)
               .HasConstraintName("FK_Proyecto_Empresa_Prestadora");
            builder.HasOne(d => d.DiaPagos).WithMany()
               .HasForeignKey(d => d.IdDiaPago)
               .HasConstraintName("FK_Proyecto_Dia_Pago");
            builder.HasMany(d => d.DocumentosSoporte)
              .WithOne(d => d.Soporte)
              .HasForeignKey(d => d.IdSoporte)
              .HasConstraintName("FK_documentos_soporte");
        }
    }
}
