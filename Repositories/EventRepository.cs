using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly UTEventsContext _context;

        public EventRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<Event?> GetEventAsync(Guid eventId)
        {
            return await _context.Events.FindAsync(eventId);
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEventAsync(Event updatedEvent)
        {
            _context.Entry(updatedEvent).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            var eventToDelete = await _context.Events.FindAsync(eventId);
            if (eventToDelete == null) return false;

            _context.Events.Remove(eventToDelete);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
