using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface ISubCategoriaRepository
    {
        Task<List<SubCategoria>> GetAllSubCategoryAsync();
        Task<List<SubCategoria>> GetSubCategoryByCategoryIdAsync(int categoryId);
        Task<SubCategoria?> GetSubCategoryByIdAsync(int id);
        Task AddAsync(SubCategoria subCategoria);
        Task UpdateAsync(SubCategoria subCategoria);
        Task DeleteAsync(SubCategoria subCategoria);
    }
}
