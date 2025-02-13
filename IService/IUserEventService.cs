using UTEvents.Enums;
using UTEvents.Models;

namespace UTEvents.IService
{
    public interface IUserEventService
    {
        Task<IEnumerable<UserEventDto>> GetUserEventsByUserIdAsync(Guid userId, RSVPStatus status);
        Task<IEnumerable<UserEventDto>> GetUserEventsByUserIdAsync(Guid userId);
        Task<IEnumerable<UserEventDto>> GetAllUserEventsAsync();
        Task<bool> AddUserEventAsync(UserEventDto userEventDto);
        Task<bool> UpdateUserEventStatusAsync(Guid userId, Guid eventId, RSVPStatus status);
    }
}
