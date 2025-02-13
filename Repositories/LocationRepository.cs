using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly UTEventsContext _context;

        public LocationRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<Location?> GetLocationAsync(Guid locationId)
        {
            return await _context.Locations.FindAsync(locationId);
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<bool> CreateLocationAsync(Location newLocation)
        {
            _context.Locations.Add(newLocation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateLocationAsync(Location updatedLocation)
        {
            _context.Entry(updatedLocation).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLocationAsync(Guid locationId)
        {
            var locationToDelete = await _context.Locations.FindAsync(locationId);
            if (locationToDelete == null) return false;

            _context.Locations.Remove(locationToDelete);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
