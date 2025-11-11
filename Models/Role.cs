namespace PlataformaEducativa.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string RoleName { get; set; } = null!;

        // Relación many-to-many con Permission
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}

