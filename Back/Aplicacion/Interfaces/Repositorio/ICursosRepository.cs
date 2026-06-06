using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface ICursosRepository
    {
        Task<List<Cursos>> GetAllCursosAsync();
        Task<Cursos?> GetCursoByIdAsync(int id);
        Task AddAsync(Cursos curso);
        Task UpdateAsync(Cursos curso);
        Task DeleteAsync(Cursos curso);
    }
}
