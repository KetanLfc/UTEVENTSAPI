using UTEvents.Entities;

namespace UTEvents.IRepository
{
    public interface ILocationRepository
    {
        Task<Location?> GetLocationAsync(Guid locationId);
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<bool> CreateLocationAsync(Location newLocation);
        Task<bool> UpdateLocationAsync(Location updatedLocation);
        Task<bool> DeleteLocationAsync(Guid locationId);
    }
}
