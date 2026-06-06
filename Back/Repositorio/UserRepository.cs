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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(
            AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(x => x.Roles).OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == name);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
