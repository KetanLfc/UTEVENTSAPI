using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.Enums;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class AllowedEventRoleRepository : IAllowedEventRoleRepository
    {
        private readonly UTEventsContext _context;

        public AllowedEventRoleRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AllowedEventRole>> GetAllowedRolesByEventIdAsync(Guid eventId)
        {
            return await _context.AllowedEventRoles
                .Where(a => a.EventId == eventId)
                .ToListAsync();
        }

        public async Task<bool> AddAllowedEventRoleAsync(AllowedEventRole allowedEventRole)
        {
            _context.AllowedEventRoles.Add(allowedEventRole);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAllowedEventRoleAsync(Guid eventId, EventRole role)
        {
            var roleToDelete = await _context.AllowedEventRoles
                .FindAsync(eventId, role);

            if (roleToDelete == null) return false;

            _context.AllowedEventRoles.Remove(roleToDelete);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
