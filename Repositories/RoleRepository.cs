using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UTEventsContext _context;

        public RoleRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task<bool> CreateRoleAsync(Role newRole)
        {
            _context.Roles.Add(newRole);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoleAsync(Guid roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return false;

            _context.Roles.Remove(role);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
