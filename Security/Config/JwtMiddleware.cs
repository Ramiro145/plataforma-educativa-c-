using Microsoft.AspNetCore.Http;
using PlataformaEducativa.Utils;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace PlataformaEducativa.Security.Config
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtUtils _jwtUtils;

        public JwtMiddleware(RequestDelegate next, JwtUtils jwtUtils)
        {
            _next = next;
            _jwtUtils = jwtUtils;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Obtener token del header Authorization
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Validar y decodificar token, devuelve ClaimsPrincipal
                    var principal = _jwtUtils.ValidateToken(token);

                    // Asignar usuario autenticado al contexto
                    context.User = principal;
                }
                catch
                {
                    // Si el token es inválido, se asigna un usuario vacío
                    context.User = new ClaimsPrincipal();
                }
            }

            // Continuar con la pipeline
            await _next(context);
        }
    }
}
