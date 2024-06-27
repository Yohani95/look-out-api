using look.domain.entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Infrastructure.data.Logger
{
    public class LogConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.ToTable("logs");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Timestamp)
                   .HasColumnName("timestamp")
                   .IsRequired();

            builder.Property(e => e.Level)
                   .HasColumnName("level")
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(e => e.SourceContext)
                   .HasColumnName("source_context")
                   .HasMaxLength(255);

            builder.Property(e => e.Message)
                   .HasColumnName("message")
                   .IsRequired();

            builder.Property(e => e.Exception)
                   .HasColumnName("exception");
        }
    }
}
