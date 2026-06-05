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
    internal class PermisoRolConfiguration : IEntityTypeConfiguration<PermisosRoles>
    {
        public void Configure(EntityTypeBuilder<PermisosRoles> builder)
        {
            builder.ToTable("PermisosRoles");

            builder.HasKey(x =>
                new
                {
                    x.RoleId,
                    x.PermissionId
                });

            builder
                .HasOne(x => x.Rol)
                .WithMany(x => x.RolPermiso)
                .HasForeignKey(x => x.RoleId);

            builder
                .HasOne(x => x.Permiso)
                .WithMany(x => x.RolPermiso)
                .HasForeignKey(x => x.PermissionId);
        }
    }
}
