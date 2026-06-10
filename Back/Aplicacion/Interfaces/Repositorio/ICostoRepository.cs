using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface ICostoRepository
    {
        Task<List<Costos>> GetAllCostAsync();
        Task<Costos?> GetCostByIdAsync(int id);
        Task AddAsync(Costos costo);
        Task UpdateAsync(Costos costo);
        Task DeleteAsync(Costos costo);
    }
}
