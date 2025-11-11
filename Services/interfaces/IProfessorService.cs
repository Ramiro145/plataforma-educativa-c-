using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProfessorService
{
    Task<List<Professor>> GetAllProfessorsAsync();
    Task<Professor?> GetProfessorByIdAsync(long id); // puede ser null si no existe
    Task<Professor> SaveProfessorAsync(CreateProfessorDTO dto);
    Task DeleteProfessorAsync(long id);
}
