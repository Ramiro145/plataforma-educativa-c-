using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Data;
using PlataformaEducativa.Models;

namespace PlataformaEducativa.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _context;

        public PermissionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Permission>> FindAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> FindByIdAsync(long id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> SaveAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Permission> UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }
    }
}
