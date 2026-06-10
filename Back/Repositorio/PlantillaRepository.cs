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
    public class PlantillaRepository : ITemplateRepository
    {
        private readonly AppDbContext _context;
        public PlantillaRepository(
            AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Templates>> GetAllTemplateAsync()
        {
            return await _context.Templates.Include(x => x.Curso).OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Templates?> GetTemplateByIdAsync(int id)
        {
            return await _context.Templates.Include(x => x.Curso)
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task AddAsync(Templates template)
        {
            await _context.Templates.AddAsync(template);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Templates template)
        {
            _context.Templates.Update(template);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Templates template)
        {
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}
