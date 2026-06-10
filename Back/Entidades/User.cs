using System.Data;

namespace Entidades
{
    public class User : EntidadBase
    {
        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; }
            = string.Empty;

        public string Email { get; set; }
            = string.Empty;

        // FK
        public int RolesId { get; set; }

        // Navigation
        public Roles Roles { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime? LastLogin { get; set; }
    }
}
