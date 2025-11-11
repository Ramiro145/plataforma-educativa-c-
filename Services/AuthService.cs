using PlataformaEducativa.Data;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using PlataformaEducativa.Utils;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Services.interfaces;

namespace PlataformaEducativa.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtUtils _jwtUtils;
        private readonly IUserService _userService;

        public AuthService(AppDbContext context, JwtUtils jwtUtils, IUserService userService)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _userService = userService;
        }

        public AuthResponseDTO LoginUser(AuthLoginRequestDTO loginDto)
        {
            var username = loginDto.Username;
            var password = loginDto.Password;

            // Autenticar usuario
            var user = Authenticate(username, password);

            // Generar JWT
            var accessToken = _jwtUtils.CreateToken(user);

            return new AuthResponseDTO(username, "login successful", accessToken, true);
        }

        private UserSec Authenticate(string username, string password)
        {
            // Cargar usuario con roles y permisos
            var user = _context.Users
                .Include(u => u.Roles)
                    .ThenInclude(r => r.Permissions)
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
                throw new Exception("Invalid username or password");

            // Verificar password
            if (!_userService.VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid username or password");

            return user;
        }
    }
}
