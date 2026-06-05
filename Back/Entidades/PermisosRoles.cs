using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PermisosRoles
    {
        public int RoleId { get; set; }

        public Roles Rol { get; set; }

        public int PermissionId { get; set; }

        public Permisos Permiso { get; set; }
    }
}
