using UTEvents.Models;

namespace UTEvents.IService
{
    public interface ILocationService
    {
        Task<LocationDto?> GetLocationAsync(Guid locationId);
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto?> CreateLocationAsync(LocationDto locationDto);
        Task<bool> UpdateLocationAsync(LocationDto locationDto);
        Task<bool> DeleteLocationAsync(Guid locationId);
    }
}
