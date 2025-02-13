using UTEvents.Enums;
using UTEvents.Models;

namespace UTEvents.IService
{
    public interface IAllowedEventRoleService
    {
        Task<IEnumerable<AllowedEventRoleDto>> GetAllowedRolesByEventIdAsync(Guid eventId);
        Task<bool> AddAllowedEventRoleAsync(AllowedEventRoleDto allowedEventRoleDto);
        Task<bool> DeleteAllowedEventRoleAsync(Guid eventId, EventRole role);
    }
}
