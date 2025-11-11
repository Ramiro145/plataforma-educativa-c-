using PlataformaEducativa.Models;

public interface IPermissionService
{
    Task<List<Permission>> FindAllAsync();
    Task<Permission?> FindByIdAsync(long id);
    Task<Permission> SaveAsync(Permission permission);
    Task DeleteByIdAsync(long id);
    Task<Permission> UpdateAsync(Permission permission);
}
