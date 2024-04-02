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
    public class HorasUtilizadasConfiguration : IEntityTypeConfiguration<HorasUtilizadas>
    {
        public void Configure(EntityTypeBuilder<HorasUtilizadas> builder)
        {

            builder.ToTable("horas_utilizadas");

            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.Property(e => e.NombreDocumento)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre_documento");
            builder.Property(e => e.ContenidoDocumento)
                .HasColumnType("longblob")
                .HasColumnName("contenido_documento");
            builder.Property(e => e.Horas)
                .HasColumnType("int")
                .HasColumnName("horas");
            builder.Property(e => e.IdSoporte)
                .HasColumnType("int")
                .HasColumnName("id_soporte");
            builder.Property(e => e.FechaPeriodoDesde)
                .HasColumnType("date")
                .HasColumnName("fecha_periodo_desde");
            builder.Property(e => e.FechaPeriodoHasta)
                .HasColumnType("date")
                .HasColumnName("fecha_periodo_hasta");
            builder.Property(e => e.Estado)
                .HasColumnType("tinyint(2)")
                .HasColumnName("estado");
            builder.Property(e => e.Monto)
                .HasColumnType("DOUBLE")
                .HasColumnName("monto");
            builder.Property(e => e.MontoHorasExtras)
                .HasColumnType("DOUBLE")
                .HasColumnName("monto_horas_extras");
            builder.Property(e => e.HorasExtras)
                .HasColumnType("int(11)")
                .HasColumnName("horas_extras"); 
            builder.Property(e => e.HorasAcumuladas)
                .HasColumnType("int(11)")
                .HasColumnName("horas_acumuladas");
            //objetos
            builder.HasOne(h => h.Soporte)
           .WithMany() // Si no hay navegación inversa en Soporte
           .HasForeignKey(h => h.IdSoporte)
           .IsRequired(false) // Si IdSoporte puede ser nulo
           .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
