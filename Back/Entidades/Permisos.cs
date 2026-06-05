using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Permisos : EntidadBase
    {
        public string NamePermission { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<PermisosRoles> RolPermiso { get; set; } = new List<PermisosRoles>();
    }
}
