using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Data;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using PlataformaEducativa.Services.interfaces;

namespace PlataformaEducativa.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateCourseAsync(CourseCreateDto dto)
        {
            var professor = await _context.Professors.FindAsync(dto.ProfessorId);
            if (professor == null)
                throw new Exception("Profesor no encontrado");

            var students = await _context.Students
                .Where(s => dto.StudentIds.Contains(s.Id))
                .ToListAsync();

            var course = new Course
            {
                CourseName = dto.CourseName,
                Professor = professor,
                Students = students
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task DeleteCourseAsync(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                             .Include(c => c.Professor)
                             .Include(c => c.Students)
                             .ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(long id)
        {
            var course = await _context.Courses
                              .Include(c => c.Professor)
                              .Include(c => c.Students)
                              .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                throw new KeyNotFoundException($"Course with Id {id} not found");

            return course;

        }

        public async Task<Course?> UpdateCourseAsync(long id, CourseUpdateDto dto)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .Include(c => c.Professor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return null;

            if (!string.IsNullOrEmpty(dto.CourseName))
                course.CourseName = dto.CourseName;

            if (dto.ProfessorId.HasValue)
            {
                var professor = await _context.Professors.FindAsync(dto.ProfessorId.Value);
                if (professor != null) course.Professor = professor;
            }

            if (dto.StudentIds != null)
            {
                var students = await _context.Students
                    .Where(s => dto.StudentIds.Contains(s.Id))
                    .ToListAsync();
                course.Students = students;
            }

            await _context.SaveChangesAsync();
            return course;
        }

    }

}
