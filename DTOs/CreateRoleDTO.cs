namespace PlataformaEducativa.DTOs
{
    public class CreateRoleDto
    {
        public string RoleName { get; set; } = null!;
        public List<long> PermissionIds { get; set; } = new();
    }

}
