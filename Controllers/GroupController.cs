using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEvents.IService;
using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetAllGroups()
        {
            return Ok(await _groupService.GetAllGroupsAsync());
        }

        [HttpGet("{groupId:Guid}")]
        public async Task<ActionResult<GroupDto>> GetGroupById(Guid groupId)
        {
            var group = await _groupService.GetGroupAsync(groupId);
            return group == null ? NotFound() : Ok(group);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GroupDto>> CreateGroup([FromBody] GroupRequest groupRequest)
        {
            var createdGroup = await _groupService.CreateGroupAsync(groupRequest);
            return createdGroup == null ? BadRequest() : Ok(createdGroup);
        }

        [HttpPut("{groupId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateGroup(Guid groupId, [FromBody] GroupRequest groupRequest)
        {
            var updated = await _groupService.UpdateGroupAsync(groupId, groupRequest);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{groupId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteGroup(Guid groupId)
        {
            var deleted = await _groupService.DeleteGroupAsync(groupId);
            return deleted ? NoContent() : BadRequest();
        }
    }
}
