using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Aplicacion.Interfaces.Repositorio
{
    public interface IPermisoRepository
    {
        Task<PermisosRoles?> GetPermisoRolAsync(
            string Rol
        );
    }
}
