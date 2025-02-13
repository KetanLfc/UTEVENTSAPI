using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEvents.Enums;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedEventRolesController : ControllerBase
    {
        private readonly IAllowedEventRoleService _service;

        public AllowedEventRolesController(IAllowedEventRoleService service)
        {
            _service = service;
        }

        [HttpGet("{eventId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AllowedEventRoleDto>>> GetAllowedRoles(Guid eventId)
        {
            return Ok(await _service.GetAllowedRolesByEventIdAsync(eventId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAllowedEventRole([FromBody] AllowedEventRoleDto roleDto)
        {
            var result = await _service.AddAllowedEventRoleAsync(roleDto);
            return result ? Ok() : BadRequest();
        }


        [HttpDelete("{eventId:Guid}/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAllowedEventRole(Guid eventId, EventRole role)
        {
            var result = await _service.DeleteAllowedEventRoleAsync(eventId, role);
            return result ? NoContent() : BadRequest();
        }
    }
}
