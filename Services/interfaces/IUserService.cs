using PlataformaEducativa.Models;
using PlataformaEducativa.DTOs;

namespace PlataformaEducativa.Services.interfaces
{
    public interface IUserService
    {
        // Listar todos los usuarios (sin password)
        Task<List<UserSec>> FindAllAsync();

        // Buscar usuario por Id
        Task<UserSec?> FindByIdAsync(long id);

        // Crear usuario a partir del DTO
        Task<UserSec> CreateUserAsync(CreateUserSecDTO dto);

        // Actualizar usuario
        Task<UserSec> UpdateAsync(long id, UpdateUserSecDTO dto);

        // Eliminar usuario por Id
        Task DeleteByIdAsync(long id);

        // Encriptar contraseña
        string EncryptPassword(string password);

        bool VerifyPassword(string plainPassword, string hashedPassword);

    }
}
