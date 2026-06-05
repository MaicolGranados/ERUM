using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface ITemplateRepository
    {
        Task<List<Templates>> GetAllTemplateAsync();
        Task<Templates?> GetTemplateByIdAsync(int id);
        Task<Templates?> GetTemplateByCodeAsync(string codigo);
        Task AddAsync(Templates template);
        Task UpdateAsync(Templates template);
        Task DeleteAsync(Templates template );
    }
}
