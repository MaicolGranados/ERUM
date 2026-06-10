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
    public class CursosConfiguration : IEntityTypeConfiguration<Cursos>
    {
        public void Configure(EntityTypeBuilder<Cursos> builder)
        {
            builder.ToTable("Cursos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(x => x.Codigo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Nombre)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Vigencia)
                .IsRequired();

            builder
                .HasOne(x => x.SubCategoria)
                .WithMany()
                .HasForeignKey(x => x.idSubCategoria);

            builder
                .HasOne(x => x.Costos)
                .WithMany()
                .HasForeignKey(x => x.idCosto);
        }
    }
}
