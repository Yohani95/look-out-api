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
            throw new NotImplementedException();
        }
    }
}
