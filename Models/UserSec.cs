namespace PlataformaEducativa.Models
{
    public class UserSec
    {
        public long Id { get; set; }

        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; // mejor usar hash que password plano

        // Campos opcionales para control de cuenta
        public bool Enabled { get; set; } = true;
        public bool AccountNotExpired { get; set; } = true;
        public bool AccountNotLocked { get; set; } = true;
        public bool CredentialNotExpired { get; set; } = true;

        // Relación many-to-many con Role
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }

}
