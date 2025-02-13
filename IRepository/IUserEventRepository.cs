using UTEvents.Entities;
using UTEvents.Enums;

namespace UTEvents.IRepository
{
    public interface IUserEventRepository
    {
        Task<IEnumerable<UserEvent>> GetUserEventsByUserIdAsync(Guid userId, RSVPStatus status);
        Task<IEnumerable<UserEvent>> GetAllUserEventsByUserIdAsync(Guid userId);
        Task<IEnumerable<UserEvent>> GetAllUserEventsAsync();
        Task<bool> AddUserEventAsync(UserEvent userEvent);
        Task<bool> UpdateUserEventStatusAsync(Guid userId, Guid eventId, RSVPStatus status);
    }
}
