using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Persistencia.Configuracion
{
    public class CostoConfiguration : IEntityTypeConfiguration<Costos>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Costos> builder)
        {
            builder.ToTable("Costos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();
            
            builder.Property(x => x.Valor)
                .IsRequired();
        }
    }
}
