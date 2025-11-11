using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Data;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProfessorService : IProfessorService
{
    private readonly AppDbContext _context;

    public ProfessorService(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todos los profesores
    public async Task<List<Professor>> GetAllProfessorsAsync()
    {
        return await _context.Professors.
                        Include(u => u.User).
                            ThenInclude(r => r.Roles).
                                ThenInclude(p => p.Permissions).
                        ToListAsync();
    }

    // Obtener profesor por Id
    public async Task<Professor?> GetProfessorByIdAsync(long id)
    {
        return await _context.Professors.
                                Include(u=>u.User)
                                    .ThenInclude(r=>r.Roles)
                                        .ThenInclude(p=>p.Permissions)
                                        . FirstOrDefaultAsync(p => p.Id == id);
    }

    // Guardar profesor (solo Name y Email)
    public async Task<Professor> SaveProfessorAsync(CreateProfessorDTO dto)
    {
        var userToAssociate = await _context.Users.FindAsync(dto.UserId);

        if (userToAssociate != null) {

            var professorToSave = new Professor
            {
                Name = dto.Name,
                Email = dto.Email,
                User = userToAssociate

            };

            _context.Professors.Add(professorToSave);
            await _context.SaveChangesAsync();

            return professorToSave;

        }

        return null;

        
    }

    // Eliminar profesor
    public async Task DeleteProfessorAsync(long id)
    {
        var professor = await _context.Professors.FindAsync(id);
        if (professor != null)
        {
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
