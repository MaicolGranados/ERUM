using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Persistencia.Configuracion
{
    public class SubCategoriaConfiguration : IEntityTypeConfiguration<SubCategoria>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SubCategoria> builder)
        {
            builder.ToTable("SubCategorias");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityAlwaysColumn();
            
            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.idCategoria);
        }
    }
}
