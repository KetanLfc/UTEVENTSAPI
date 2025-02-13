using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.Enums;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly UTEventsContext _context;

        public UserEventRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEvent>> GetUserEventsByUserIdAsync(Guid userId, RSVPStatus status)
        {
            return await _context.UserEvents
                .Where(ue => ue.UserId == userId && ue.RSVPStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserEvent>> GetAllUserEventsByUserIdAsync(Guid userId)
        {
            return await _context.UserEvents
                .Where(ue => ue.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserEvent>> GetAllUserEventsAsync()
        {
            return await _context.UserEvents.ToListAsync();
        }

        public async Task<bool> AddUserEventAsync(UserEvent userEvent)
        {
            _context.UserEvents.Add(userEvent);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserEventStatusAsync(Guid userId, Guid eventId, RSVPStatus status)
        {
            var userEvent = await _context.UserEvents
                .FindAsync(userId, eventId);

            if (userEvent == null) return false;

            userEvent.RSVPStatus = status;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
