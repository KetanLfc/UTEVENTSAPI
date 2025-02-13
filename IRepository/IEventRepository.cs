using UTEvents.Entities;

namespace UTEvents.IRepository
{
    public interface IEventRepository
    {
        Task<Event?> GetEventAsync(Guid eventId);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<bool> CreateEventAsync(Event newEvent);
        Task<bool> UpdateEventAsync(Event updatedEvent);
        Task<bool> DeleteEventAsync(Guid eventId);
    }
}
