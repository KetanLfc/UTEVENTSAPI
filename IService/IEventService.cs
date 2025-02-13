using UTEvents.Models;

namespace UTEvents.IService
{
    public interface IEventService
    {
        Task<EventDto?> GetEventAsync(Guid eventId);
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto?> CreateEventAsync(EventDto eventDto);
        Task<bool> UpdateEventAsync(EventDto eventDto);
        Task<bool> DeleteEventAsync(Guid eventId);
    }
}
