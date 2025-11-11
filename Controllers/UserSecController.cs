using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using PlataformaEducativa.Services;
using PlataformaEducativa.Services.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaEducativa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Por defecto requiere autenticación
    public class UserSecController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserSecController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: api/users
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<UserSec>>> GetAllUsers()
        {
            var users = await _userService.FindAllAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<UserSec>> GetUserById(long id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<UserSec>> CreateUser([FromBody] CreateUserSecDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }
    }
}
