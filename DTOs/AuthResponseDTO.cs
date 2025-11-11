using System.Text.Json.Serialization;

namespace PlataformaEducativa.DTOs
{
    public class AuthResponseDTO
    {
        [JsonPropertyOrder(0)]
        public string Username { get; set; } = null!;

        [JsonPropertyOrder(1)]
        public string Message { get; set; } = null!;

        [JsonPropertyOrder(2)]
        public string Jwt { get; set; } = null!;

        [JsonPropertyOrder(3)]
        public bool Status { get; set; }

        public AuthResponseDTO(string username, string message, string jwt, bool status)
        {
            Username = username;
            Message = message;
            Jwt = jwt;
            Status = status;
        }
    }
}
