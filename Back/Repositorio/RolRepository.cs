using Aplicacion.Interfaces.Repositorio;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDbContext _context;
        public RolRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Roles?>GetByIdAsync(int id)
        {
            return await _context.Roles
                .Include(x =>
                    x.RolPermiso)
                .ThenInclude(x =>
                    x.Permiso)
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task<Roles?>GetByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x =>
                    x.NameRole == name);
        }

        public async Task<List<Roles>>GetAllAsync()
        {
            return await _context.Roles.OrderBy(x => x.Id)
                .ToListAsync();
        }
    }
}
