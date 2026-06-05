using Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Interfaces.Repositorio;

namespace Repositorio
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AppDbContext _context;
        public PermisoRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<PermisosRoles?> GetPermisoRolAsync(string rol)
        {
            return await _context.PermisosRoles.Include(x => x.Rol)
                .FirstOrDefaultAsync(x =>
                    x.Rol.NameRole == rol);
        }
    }
}
