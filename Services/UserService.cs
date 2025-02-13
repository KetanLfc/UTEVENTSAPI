using AutoMapper;
using UTEvents.Entities;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;
using UTEvents.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using UTEvents.Repositories;

namespace UTEvents.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }


        public async Task<UserDto?> CreateUserAsync(UserRequest userRequest)
        {
            // Check if email is already registered
            var existingUser = await _userRepository.GetUserByEmailAsync(userRequest.Email);
            if (existingUser != null)
                return null; // Email already exists

            var user = _mapper.Map<User>(userRequest);
            user.IsActive = true; // Set as active by default
            user.EmailConfirmed = false;

            if (userRequest.RoleId == Guid.Empty)
            {
                // fetch the "Student" role from the DB
                var studentRole = await GetStudentRoleAsync();
                if (studentRole != null)
                {
                    user.RoleId = studentRole.Id;
                }
            }
            else
            {
                // userRequest provided a valid RoleId;
                user.RoleId = userRequest.RoleId;
            }

            // Hash the password before saving
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, userRequest.Password);

            var created = await _userRepository.CreateUserAsync(user);
            return created ? _mapper.Map<UserDto>(user) : null;
        }

        private async Task<Role?> GetStudentRoleAsync()
        {
            return await _roleRepository.GetRoleByNameAsync("Student");
        }

        public async Task<bool> UpdateUserAsync(UserRequest userRequest)
        {
            // 1. Fetch existing user from the repository
            if (!userRequest.Id.HasValue)
                return false; // must have a valid user ID

            var existingUser = await _userRepository.GetUserByIdAsync(userRequest.Id.Value);
            if (existingUser == null)
                return false; // no user found

            // 2. Update fields that can change
            existingUser.Email = userRequest.Email;
            existingUser.Name = userRequest.Name;
            existingUser.RoleId = userRequest.RoleId;
            existingUser.GroupId = userRequest.GroupId;
            existingUser.IsActive = userRequest.IsActive;

            // 3. If a new password was provided, re-hash and set it
            if (!string.IsNullOrEmpty(userRequest.Password))
            {
                var passwordHasher = new PasswordHasher<User>();
                existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, userRequest.Password);
            }

            // 4. Call repo to persist
            return await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public string GenerateJwtToken(UserDto userDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"); 

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
        new Claim(ClaimTypes.Email, userDto.Email),
        new Claim(ClaimTypes.Role, userDto.RoleName) // Include the user's role
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "UTEventsAPI",
                Audience = "UTEventsUI"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string?> LoginUserAsync(string email, string password)
        {
            // Retrieve user entity from the database
            var userEntity = await _userRepository.GetUserByEmailAsync(email);
            if (userEntity == null || !userEntity.IsActive)
                return null; // invalid user or inactive

            // Verify password
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(
                userEntity,
                userEntity.PasswordHash,
                password
            );

            if (result == PasswordVerificationResult.Failed)
                return null; // incorrect password

            // Map entity to dto for token generation
            var userDto = _mapper.Map<UserDto>(userEntity);

            // Generate token and return
            return GenerateJwtToken(userDto);
        }
    }
}
