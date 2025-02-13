using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTEvents.Enums;
using UTEvents.IService;
using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private readonly IUserEventService _service;

        public UserEventsController(IUserEventService service)
        {
            _service = service;
        }
    
        [HttpGet("{userId:Guid}/{status}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserEventDto>>> GetUserEvents(Guid userId, RSVPStatus status)
        {
            if (!IsUserAuthorized(userId))
                return Forbid("You can only access your own RSVP events.");

            var userEvents = await _service.GetUserEventsByUserIdAsync(userId, status);
            return Ok(userEvents);
        }

        [HttpGet("{userId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserEventDto>>> GetAllUserEventsForUser(Guid userId)
        {
            if (!IsUserAuthorized(userId))
                return Forbid("You can only access your own RSVP events.");

            // Call service method to get all events for that user
            var userEvents = await _service.GetUserEventsByUserIdAsync(userId);
            return Ok(userEvents);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserEventDto>>> GetAllUserEvents()
        {
            // Only admins allowed, so no userID checking
            var allUserEvents = await _service.GetAllUserEventsAsync();
            return Ok(allUserEvents);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddUserEvent([FromBody] UserEventDto userEventDto)
        {
            if (!IsUserAuthorized(userEventDto.UserId))
                return Forbid("You can only RSVP to events for yourself.");

            var result = await _service.AddUserEventAsync(userEventDto);
            return result ? Ok() : BadRequest("Failed to add user event.");
        }

        [HttpPut("{userId:Guid}/{eventId:Guid}")]
        [Authorize]
        public async Task<ActionResult> UpdateUserEventStatus(Guid userId, Guid eventId, [FromBody] RSVPRequest request)
        {
            if (!IsUserAuthorized(userId))
                return Forbid("You can only update your own RSVP status.");

            // We only need the Status property:
            var result = await _service.UpdateUserEventStatusAsync(userId, eventId, request.Status);
            return result ? NoContent() : BadRequest("Failed to update RSVP status.");
        }


        [HttpDelete("{userId:Guid}/{eventId:Guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteUserEvent(Guid userId, Guid eventId)
        {
            if (!IsUserAuthorized(userId))
                return Forbid("You can only cancel your own RSVP.");

            var result = await _service.UpdateUserEventStatusAsync(userId, eventId, RSVPStatus.NotGoing);
            return result ? NoContent() : BadRequest("Failed to delete RSVP.");
        }

        // Helper Method to Validate UserId
        private bool IsUserAuthorized(Guid userId)
        {
            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Retrieve UserId from JWT claims
            return loggedInUserId != null && loggedInUserId == userId.ToString();
        }
    }
}
