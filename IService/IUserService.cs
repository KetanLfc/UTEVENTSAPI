using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid userId);
        Task<UserDto?> CreateUserAsync(UserRequest userRequest);
        Task<bool> UpdateUserAsync(UserRequest userRequest);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<UserDto?> GetUserByEmailAsync(string email);
        string GenerateJwtToken(UserDto user);
        Task<string?> LoginUserAsync(string email, string password);
    }
}
