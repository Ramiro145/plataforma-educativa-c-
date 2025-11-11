using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using PlataformaEducativa.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEducativa.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Esto hará que la ruta sea /api/students
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        // GET /api/students
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // GET /api/students/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(long id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST /api/students
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Student>> SaveStudent([FromBody] CreateStudentDTO student)
        {
            var createdStudent = await _studentService.SaveStudentAsync(student);

            if(createdStudent == null)
            {
                return BadRequest("Usuario con ese id de usuario no econtrado");
            }

            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
        }

        // DELETE /api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
