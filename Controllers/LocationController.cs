using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations()
        {
            return Ok(await _locationService.GetAllLocationsAsync());
        }

        [HttpGet("{locationId:Guid}")]
        public async Task<ActionResult<LocationDto>> GetLocation(Guid locationId)
        {
            var locationDto = await _locationService.GetLocationAsync(locationId);
            return locationDto == null ? NotFound() : Ok(locationDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] LocationDto locationDto)
        {
            var createdLocation = await _locationService.CreateLocationAsync(locationDto);
            return createdLocation == null ? BadRequest() : Ok(createdLocation);
        }

        [HttpPut("{locationId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateLocation(Guid locationId, [FromBody] LocationDto locationDto)
        {
            var updated = await _locationService.UpdateLocationAsync(locationDto with { LocationId = locationId });
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{locationId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteLocation(Guid locationId)
        {
            var deleted = await _locationService.DeleteLocationAsync(locationId);
            return deleted ? NoContent() : BadRequest();
        }
    }
}
