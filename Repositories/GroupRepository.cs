using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UTEventsContext _context;

        public GroupRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group?> GetGroupByIdAsync(Guid groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<bool> CreateGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGroupAsync(Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGroupAsync(Guid groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            if (group == null) return false;

            _context.Groups.Remove(group);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
