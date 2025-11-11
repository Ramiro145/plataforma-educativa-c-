using Microsoft.AspNetCore.Authorization;

namespace PlataformaEducativa.Security.Config.Authorization
{
    public class CourseProfessorRequirement : IAuthorizationRequirement
    {
        public long CourseId { get; }
        public CourseProfessorRequirement(long courseId)
        {
            CourseId = courseId;
        }
    }

}
