using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.DTOs
{
    public class AuthLoginRequestDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
