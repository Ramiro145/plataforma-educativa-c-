using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using BCrypt.Net;
namespace PlataformaEducativa.Services
{
    using Microsoft.EntityFrameworkCore;
    using PlataformaEducativa.Data;
    using PlataformaEducativa.Services.interfaces;
    using System.Security.Cryptography;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserSec>> FindAllAsync()
        {
            return await _context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Permissions)
                .ToListAsync();
        }

        public async Task<UserSec?> FindByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserSec> CreateUserAsync(CreateUserSecDTO dto)
        {
            // Crear instancia de usuario
            var user = new UserSec
            {
                Username = dto.Username,
                PasswordHash = EncryptPassword(dto.Password),
                Enabled = true,
                AccountNotExpired = true,
                AccountNotLocked = true,
                CredentialNotExpired = true
            };

            // Asignar rol si existe
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == dto.Role);
            if (role != null)
            {
                user.Roles.Add(role);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserSec> UpdateAsync(long id, UpdateUserSecDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new Exception("Usuario no encontrado");

            if (!string.IsNullOrEmpty(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = EncryptPassword(dto.Password);

            if (dto.Enabled.HasValue)
                user.Enabled = dto.Enabled.Value;

            if (!string.IsNullOrEmpty(dto.Role))
            {
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.RoleName == dto.Role);
                if (role != null)
                {
                    user.Roles.Clear();
                    user.Roles.Add(role);
                }
            }

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public string EncryptPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }

}
