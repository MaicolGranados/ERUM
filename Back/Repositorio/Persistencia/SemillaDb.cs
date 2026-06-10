using BCrypt.Net;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Persistencia
{
    public class SemillaDb
    {
        public static async Task SeedAsync(
        AppDbContext context)
        {
            await context.Database.MigrateAsync();

            var adminExists = await context.Users
                .AnyAsync(x => x.Username == "admin");

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Roles
                    {
                        NameRole = "Administrator"
                    });

                await context.SaveChangesAsync();
            }

            var adminRole = await context.Roles
            .FirstAsync(x => x.NameRole == "Administrator");

            if (!adminExists)
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@fundacionerum.org.co",
                    RolesId = adminRole.Id,

                    PasswordHash =
                        BCrypt.Net.BCrypt.HashPassword(
                            "Admin123*"
                        ),

                    IsActive = true
                };

                await context.Users.AddAsync(
                    adminUser);

                await context.SaveChangesAsync();
            }

        }
    }
}
