using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UTEvents.IService;
using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{userId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserRequest userRequest)
        {
            // Enforce Password
            if (string.IsNullOrEmpty(userRequest.Password))
                return BadRequest("Password is required for registration.");

            // Validate email domain
            if (!IsValidUniversityEmail(userRequest.Email))
                return BadRequest("Only university email addresses (@umail.utm.ac.mu or @utm.ac.mu) are allowed.");

            // Create user
            var user = await _userService.CreateUserAsync(userRequest);
            return user == null ? BadRequest("Registration failed. Please check your details.") : Ok(user);
        }

        // Helper method to validate the email domain
        private bool IsValidUniversityEmail(string email)
        {
            return email.EndsWith("@umail.utm.ac.mu") || email.EndsWith("@utm.ac.mu");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _userService.LoginUserAsync(loginRequest.Email, loginRequest.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }


        [HttpPut("{userId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUser(Guid userId, [FromBody] UserRequest userRequest)
        {
            userRequest = userRequest with { Id = userId };
            var updated = await _userService.UpdateUserAsync(userRequest);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{userId:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            return deleted ? NoContent() : BadRequest();
        }
    }
}
