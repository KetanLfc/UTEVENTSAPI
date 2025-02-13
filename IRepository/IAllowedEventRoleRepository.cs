using UTEvents.Entities;
using UTEvents.Enums;

namespace UTEvents.IRepository
{
    public interface IAllowedEventRoleRepository
    {
        Task<IEnumerable<AllowedEventRole>> GetAllowedRolesByEventIdAsync(Guid eventId);
        Task<bool> AddAllowedEventRoleAsync(AllowedEventRole allowedEventRole);
        Task<bool> DeleteAllowedEventRoleAsync(Guid eventId, EventRole role);
    }
}
