using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlataformaEducativa.Data;
using PlataformaEducativa.Utils;
using System.Security.Claims;

namespace PlataformaEducativa.Security.Config.Authorization
{
    public class CourseProfessorHandler : AuthorizationHandler<CourseProfessorRequirement>
    {
        private readonly AppDbContext _context;
        private readonly JwtUtils _jwtUtils;
        private readonly ILogger<CourseProfessorHandler> _logger;

        public CourseProfessorHandler(AppDbContext context, JwtUtils jwtUtils, ILogger<CourseProfessorHandler> logger)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CourseProfessorRequirement requirement)
        {
            var username = _jwtUtils.ExtractUsername(context.User);

            if (string.IsNullOrEmpty(username))
            {
                _logger.LogWarning("Authorization failed: username is null or empty.");
                return;
            }

            var course = await _context.Courses
                .Include(c => c.Professor)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(c => c.Id == requirement.CourseId);

            if (course?.Professor?.User?.Username?.Equals(username, StringComparison.OrdinalIgnoreCase) == true)
            {
                context.Succeed(requirement);
                _logger.LogInformation($"Authorization succeeded for user '{username}' on course {requirement.CourseId}.");
            }
            else
            {
                _logger.LogWarning($"Authorization failed for user '{username}' on course {requirement.CourseId}.");
            }
        }

    }
}
