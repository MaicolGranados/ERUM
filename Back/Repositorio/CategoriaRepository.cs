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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(
            AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Categoria>> GetAllCategoryAsync()
        {
            return await _context.Categorias.OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Categoria?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
