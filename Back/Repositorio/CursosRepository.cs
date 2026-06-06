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
    public class CursosRepository : ICursosRepository
    {
        private readonly AppDbContext _context;
        public CursosRepository(
            AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Cursos>> GetAllCursosAsync()
        {
            return await _context.Cursos.OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Cursos?> GetCursoByIdAsync(int id)
        {
            return await _context.Cursos
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task AddAsync(Cursos curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cursos curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Cursos curso)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
        }
    }
}
