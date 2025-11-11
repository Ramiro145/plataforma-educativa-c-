using PlataformaEducativa.Models;
using PlataformaEducativa.DTOs;

namespace PlataformaEducativa.Services.interfaces
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<Course> GetCourseByIdAsync(long id);
        public Task<Course> CreateCourseAsync(CourseCreateDto dto);
        public Task<Course> UpdateCourseAsync(long id, CourseUpdateDto dto);
        public Task DeleteCourseAsync(long id);
    }
}
