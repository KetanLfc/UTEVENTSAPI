using Microsoft.AspNetCore.Mvc;
using UTEvents.Models;
using UTEvents.Requests;
using UTEvents.IService;
using Microsoft.AspNetCore.Authorization;
namespace UTEvents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            return Ok(await _eventService.GetAllEventsAsync());
        }

        [HttpGet("{eventId:Guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<EventDto>> GetEvent(Guid eventId)
        {
            var eventDto = await _eventService.GetEventAsync(eventId);
            return eventDto == null ? NotFound() : Ok(eventDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EventDto>> CreateEvent([FromBody] EventRequest eventRequest)
        {
            var eventDto = new EventDto
            {
                Name = eventRequest.Name,
                CategoryName = eventRequest.CategoryName,
                StartDateTime = eventRequest.StartDateTime,
                EndDateTime = eventRequest.EndDateTime,
                Description = eventRequest.Description,
                ImageUrl = eventRequest.ImageUrl,
                Status = eventRequest.Status,
                Scope = eventRequest.Scope,
                LocationId = eventRequest.LocationId
            };

            var createdEvent = await _eventService.CreateEventAsync(eventDto);
            return createdEvent == null ? BadRequest() : Ok(createdEvent);
        }

        [HttpPut("{eventId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateEvent(Guid eventId, [FromBody] EventRequest eventRequest)
        {
            var eventDto = new EventDto
            {
                Id = eventId,
                Name = eventRequest.Name,
                CategoryName = eventRequest.CategoryName,
                StartDateTime = eventRequest.StartDateTime,
                EndDateTime = eventRequest.EndDateTime,
                Description = eventRequest.Description,
                ImageUrl = eventRequest.ImageUrl,
                Status = eventRequest.Status,
                Scope = eventRequest.Scope,
                LocationId = eventRequest.LocationId
            };

            var updated = await _eventService.UpdateEventAsync(eventDto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{eventId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEvent(Guid eventId)
        {
            var deleted = await _eventService.DeleteEventAsync(eventId);
            return deleted ? NoContent() : BadRequest();
        }
    }
}
