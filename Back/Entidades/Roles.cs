namespace Entidades
{
    public class Roles : EntidadBase
    {
        public string NameRole { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<PermisosRoles> RolPermiso { get; set; } = new List<PermisosRoles>();
    }
}
