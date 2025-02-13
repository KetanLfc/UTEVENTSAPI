using UTEvents.Models;

namespace UTEvents.IService
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto?> GetRoleByIdAsync(Guid roleId);
        Task<RoleDto?> CreateRoleAsync(RoleDto roleDto);
        Task<bool> UpdateRoleAsync(Guid roleId, RoleDto roleDto);
        Task<bool> DeleteRoleAsync(Guid roleId);
    }
}
