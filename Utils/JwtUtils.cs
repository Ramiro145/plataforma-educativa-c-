using Microsoft.IdentityModel.Tokens;
using PlataformaEducativa.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PlataformaEducativa.Utils
{
    public class JwtUtils
    {
        private readonly string _privateKey;
        private readonly string _userGenerator;

        public JwtUtils(string privateKey, string userGenerator)
        {
            _privateKey = privateKey;
            _userGenerator = userGenerator;
        }

        // Genera token con roles y permisos (equivalente a Spring Security)
        public string CreateToken(UserSec user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Roles -> ClaimTypes.Role
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName)); // como en Spring
            }

            // Permisos -> claim personalizado "Permission"
            foreach (var permission in user.Roles.SelectMany(r => r.Permissions))
            {
                claims.Add(new Claim("Permission", permission.PermissionName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_privateKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _userGenerator,
                audience: _userGenerator,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_privateKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _userGenerator,
                ValidAudience = _userGenerator,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return principal;
        }


        // Extraer username (equivalente a getSubject)
        public string ExtractUsername(ClaimsPrincipal principal)
        {
            return principal.Identity?.Name ?? "";
        }

        // Extraer claim específica
        public string? GetSpecificClaim(ClaimsPrincipal principal, string claimName)
        {
            return principal.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
        }

        // Extraer todas las claims
        public Dictionary<string, string> ReturnAllClaims(ClaimsPrincipal principal)
        {
            return principal.Claims.ToDictionary(c => c.Type, c => c.Value);
        }
    }
}
