using PlataformaEducativa.Models;
using PlataformaEducativa.DTOs;
public interface IRoleService
{
    Task<List<Role>> FindAllAsync();
    Task<Role?> FindByIdAsync(long id);
    Task<Role> SaveAsync(CreateRoleDto dto);
    Task DeleteByIdAsync(long id);
    Task<Role> UpdateAsync(CreateRoleDto dto, long id);
}
