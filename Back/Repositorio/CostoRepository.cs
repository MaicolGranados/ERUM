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
    public class CostoRepository : ICostoRepository
    {
        private readonly AppDbContext _context;
        public CostoRepository(
            AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Costos>> GetAllCostAsync()
        {
            return await _context.Costos.OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Costos?> GetCostByIdAsync(int id)
        {
            return await _context.Costos
                .FirstOrDefaultAsync(x =>
                    x.Id == id);
        }

        public async Task AddAsync(Costos costo)
        {
            await _context.Costos.AddAsync(costo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Costos costo)
        {
            _context.Costos.Update(costo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Costos costo)
        {
            _context.Costos.Remove(costo);
            await _context.SaveChangesAsync();
        }
    }
}
