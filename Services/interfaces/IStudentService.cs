using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStudentService
{
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(long id); // puede ser null si no existe
    Task<Student> SaveStudentAsync(CreateStudentDTO dto);
    Task DeleteStudentAsync(long id);
}
