using UTEvents.Entities;

namespace UTEvents.IRepository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(Guid roleId);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<bool> CreateRoleAsync(Role newRole);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(Guid roleId);
    }
}
