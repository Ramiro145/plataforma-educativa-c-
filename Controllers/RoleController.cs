using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Models;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Services.interfaces;

namespace PlataformaEducativa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Por defecto, requiere autenticación
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public RoleController(IRoleService roleService, IPermissionService permissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
        }

        // GET: api/roles
        [HttpGet]
        [AllowAnonymous] // equivalente a @PreAuthorize("permitAll()")
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            var roles = await _roleService.FindAllAsync();
            return Ok(roles);
        }

        // GET: api/roles/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Role>> GetRoleById(long id)
        {
            var role = await _roleService.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        // POST: api/roles
        [HttpPost]
        [Authorize(Roles = "ADMIN")] // equivalente a @PreAuthorize("hasAnyRole('ADMIN')")
        public async Task<ActionResult<Role>> CreateRole([FromBody] CreateRoleDto dto)
        {
            var newRole = await _roleService.SaveAsync(dto);
            return Ok(newRole);
        }

        // PATCH: api/roles/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Role>> PatchRole([FromBody] CreateRoleDto dto, long id)
        {
            var updated = await _roleService.UpdateAsync(dto, id);
            return Ok(updated);
        }
    }
}
