namespace PlataformaEducativa.DTOs
{
    public class UpdateUserSecDTO
    {
        public string? Username { get; set; }          // nullable: solo se cambia si viene
        public string? Password { get; set; }          // nullable: solo se cambia si viene
        public bool? Enabled { get; set; }             // nullable: permite no tocarlo
        public string? Role { get; set; }              // nullable: permite actualizar el rol
    }

}
