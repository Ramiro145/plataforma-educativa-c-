using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Data;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todos los estudiantes
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.
                        Include(u => u.User).
                            ThenInclude(r => r.Roles).
                                ThenInclude(p => p.Permissions).
                        ToListAsync();
    }

    // Obtener estudiante por Id
    public async Task<Student?> GetStudentByIdAsync(long id)
    {
        return await _context.Students.
                                Include(u => u.User)
                                    .ThenInclude(r => r.Roles)
                                        .ThenInclude(p => p.Permissions)
                                        .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Guardar estudiante (solo Name y Email)
    public async Task<Student> SaveStudentAsync(CreateStudentDTO dto) 
    {

        var userToassociate = await _context.Users.FindAsync(dto.UserId);

        if(userToassociate != null)
        {
            var studentToSave = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                User = userToassociate
            };

            _context.Students.Add(studentToSave);
            await _context.SaveChangesAsync();

            return studentToSave;

        }

        return null;
    }

    // Eliminar estudiante
    public async Task DeleteStudentAsync(long id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
