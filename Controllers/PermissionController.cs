using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Models;
using PlataformaEducativa.Services.interfaces;

namespace PlataformaEducativa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET: api/permissions
        [HttpGet]
        [AllowAnonymous] // equivale a @PreAuthorize("permitAll()")
        public async Task<ActionResult<IEnumerable<Permission>>> GetAllPermissions()
        {
            var permissions = await _permissionService.FindAllAsync();
            return Ok(permissions);
        }

        // GET: api/permissions/{id}
        [HttpGet("{id}")]
        [AllowAnonymous] 
        public async Task<ActionResult<Permission>> GetPermissionById(long id)
        {
            var permission = await _permissionService.FindByIdAsync(id);
            if (permission == null)
                return NotFound();

            return Ok(permission);
        }

        // POST: api/permissions
        [HttpPost]
        [Authorize(Roles = "ADMIN")] // equivale a @PreAuthorize("hasRole('ADMIN')")
        public async Task<ActionResult<Permission>> CreatePermission([FromBody] Permission permission)
        {
            var newPermission = await _permissionService.SaveAsync(permission);
            return Ok(newPermission);
        }
    }
}
