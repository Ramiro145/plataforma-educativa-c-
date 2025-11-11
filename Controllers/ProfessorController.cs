using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using PlataformaEducativa.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEducativa.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Esto hará que la ruta sea /api/professor
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorService _professorService;

        public ProfessorController(ProfessorService professorService)
        {
            _professorService = professorService;
        }

        // GET /api/professor
        [HttpGet]
        public async Task<ActionResult<List<Professor>>> GetAllProfessors()
        {
            var professors = await _professorService.GetAllProfessorsAsync();
            return Ok(professors);
        }

        // GET /api/professor/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessorById(long id)
        {
            var professor = await _professorService.GetProfessorByIdAsync(id);
            if (professor == null)
                return NotFound();

            return Ok(professor);
        }

        // POST /api/professor
        [HttpPost]
        public async Task<ActionResult<Professor>> SaveProfessor([FromBody] CreateProfessorDTO professor)
        {

            var createdProfessor = await _professorService.SaveProfessorAsync(professor);

            if(createdProfessor == null)
            {
                return BadRequest("Professor con ese id no encontrado");
            }

            return CreatedAtAction(nameof(GetProfessorById), new { id = createdProfessor.Id }, createdProfessor);
        }

        // DELETE /api/professor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(long id)
        {
            await _professorService.DeleteProfessorAsync(id);
            return NoContent();
        }
    }
}
