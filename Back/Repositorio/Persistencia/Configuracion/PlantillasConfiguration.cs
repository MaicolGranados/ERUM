using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Persistencia.Configuracion
{
    public class PlantillasConfiguration : IEntityTypeConfiguration<Templates>
    {
        public void Configure(EntityTypeBuilder<Templates> builder)
        {
            builder.ToTable("Plantillas");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(x => x.Html)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Imagen)
                .HasColumnType("text")
                .IsRequired();

            builder
               .HasOne(x => x.Curso)
               .WithMany()
               .HasForeignKey(x => x.CursoId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
