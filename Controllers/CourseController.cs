using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using PlataformaEducativa.Security.Config.Authorization;
using PlataformaEducativa.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEducativa.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Esto hará que la ruta sea /api/courses
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET /api/courses
        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        // GET /api/courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(long id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // POST /api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> SaveCourse([FromBody] CourseCreateDto dto)
        {
            var course = await _courseService.CreateCourseAsync(dto);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        // DELETE /api/courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }

        // PUT /api/courses/{id}
        [Authorize( Roles ="PROFESSOR")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(long id, [FromBody] CourseUpdateDto dto, [FromServices] IAuthorizationService authService)
        {
            var requirement = new CourseProfessorRequirement(id);
            var authResult = await authService.AuthorizeAsync(User, null, requirement);

            if (!authResult.Succeeded)
                return Forbid();

            var course = await _courseService.UpdateCourseAsync(id, dto);
            if (course == null) return NotFound();
            return Ok(course);
        }
    }
}
