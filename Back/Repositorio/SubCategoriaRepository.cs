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
    public class SubCategoriaRepository : ISubCategoriaRepository
    {
        private readonly AppDbContext _context;
        public SubCategoriaRepository(
            AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<SubCategoria>> GetAllSubCategoryAsync()
        {
            return await _context.SubCategorias.OrderBy(x => x.Id).Include(x => x.Categoria)
                .ToListAsync();
        }

        public async Task<List<SubCategoria>> GetSubCategoryByCategoryIdAsync(int categoryId)
        {
            return await _context.SubCategorias.Where(x => x.idCategoria == categoryId)
                .Include(x => x.Categoria).ToListAsync();
        }

        public async Task<SubCategoria?> GetSubCategoryByIdAsync(int id)
        {
            return await _context.SubCategorias.Include(x => x.Categoria)
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task AddAsync(SubCategoria subCategoria)
        {
            await _context.SubCategorias.AddAsync(subCategoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubCategoria subCategoria)
        {
            _context.SubCategorias.Update(subCategoria);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(SubCategoria subCategoria)
        {
            _context.SubCategorias.Remove(subCategoria);
            await _context.SaveChangesAsync();
        }
        
    }
}
