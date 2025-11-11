using PlataformaEducativa.Data;
using PlataformaEducativa.DTOs;
using PlataformaEducativa.Models;
using Microsoft.EntityFrameworkCore;


namespace PlataformaEducativa.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> FindAllAsync()
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .ToListAsync();
        }

        public async Task<Role?> FindByIdAsync(long id)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> SaveAsync(CreateRoleDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName
            };

            var permissions = await _context.Permissions
                .Where(p => dto.PermissionIds.Contains(p.Id))
                .ToListAsync();

            role.Permissions = new HashSet<Permission>(permissions);

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role> UpdateAsync(CreateRoleDto dto, long id)
        {
            var role = await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role == null) throw new Exception("Role not found");

            role.RoleName = dto.RoleName;

            var permissions = await _context.Permissions
                .Where(p => dto.PermissionIds.Contains(p.Id))
                .ToListAsync();

            role.Permissions = new HashSet<Permission>(permissions);

            await _context.SaveChangesAsync();
            return role;
        }
    }

}
