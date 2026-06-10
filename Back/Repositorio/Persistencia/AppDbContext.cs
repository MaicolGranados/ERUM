using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Persistencia
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<Permisos> Permisos => Set<Permisos>();
        public DbSet<PermisosRoles> PermisosRoles => Set<PermisosRoles>();
        public DbSet<Templates> Templates => Set<Templates>();
        public DbSet<Cursos> Cursos => Set<Cursos>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<SubCategoria> SubCategorias => Set<SubCategoria>();
        public DbSet<Costos> Costos => Set<Costos>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly
            );
        }
        public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<EntidadBase>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
