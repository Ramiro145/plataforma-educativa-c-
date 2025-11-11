using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace PlataformaEducativa.Security.Config.Authorization
{
    // revisar imp de policies

    //public static class AuthorizationPoliciesExtension
    //{
    //    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    //    {
    //        services.AddAuthorization(options =>
    //        {
    //            options.AddPolicy("ProfessorOfCoursePolicy", policy =>
    //            {
    //                policy.RequireAuthenticatedUser();
    //                // TODO: Re-enable after confirming role name in database
    //                policy.RequireRole("PROFESSOR");
    //                policy.Requirements.Add(new CourseProfessorRequirement());
    //            });
    //        });

    //        return services;
    //    }
    //}
}
