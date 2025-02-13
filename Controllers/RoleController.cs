using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        [HttpGet("{roleId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoleDto>> GetRoleById(Guid roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] RoleDto roleDto)
        {
            var createdRole = await _roleService.CreateRoleAsync(roleDto);
            return createdRole == null ? BadRequest() : Ok(createdRole);
        }

        [HttpPut("{roleId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateRole(Guid roleId, [FromBody] RoleDto roleDto)
        {
            var updated = await _roleService.UpdateRoleAsync(roleId, roleDto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{roleId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRole(Guid roleId)
        {
            var deleted = await _roleService.DeleteRoleAsync(roleId);
            return deleted ? NoContent() : BadRequest();
        }
    }
}
