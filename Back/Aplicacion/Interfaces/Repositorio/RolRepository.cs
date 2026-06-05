using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface IRolRepository
    {
        Task<Roles?> GetByIdAsync(int id);

        Task<Roles?> GetByNameAsync(string name);

        Task<List<Roles>> GetAllAsync();
    }
}
