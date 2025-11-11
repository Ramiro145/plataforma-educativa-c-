namespace PlataformaEducativa.DTOs
{
    public class CreateProfessorDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long UserId { get; set; }  // Asociar al usuario existente
    }
}
