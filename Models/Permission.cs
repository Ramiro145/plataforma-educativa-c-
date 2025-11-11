namespace PlataformaEducativa.Models
{
    public class Permission
    {
        public long Id { get; set; }  // EF Core lo detecta como PK por convención

        public string PermissionName { get; set; } = null!; // no nullable, único se configura en Fluent API
    }
}
