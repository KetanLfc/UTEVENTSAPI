using AutoMapper;
using UTEvents.Entities;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<LocationDto?> GetLocationAsync(Guid locationId)
        {
            var locationEntity = await _locationRepository.GetLocationAsync(locationId);
            return locationEntity == null ? null : _mapper.Map<LocationDto>(locationEntity);
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllLocationsAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto?> CreateLocationAsync(LocationDto locationDto)
        {
            var locationEntity = _mapper.Map<Location>(locationDto);
            var created = await _locationRepository.CreateLocationAsync(locationEntity);
            return created ? locationDto : null;
        }

        public async Task<bool> UpdateLocationAsync(LocationDto locationDto)
        {
            var locationEntity = _mapper.Map<Location>(locationDto);
            return await _locationRepository.UpdateLocationAsync(locationEntity);
        }

        public async Task<bool> DeleteLocationAsync(Guid locationId)
        {
            return await _locationRepository.DeleteLocationAsync(locationId);
        }
    }
}
