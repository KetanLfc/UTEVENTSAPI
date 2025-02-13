namespace UTEvents.Models
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public Guid RoleId { get; init; }
        public Guid? GroupId { get; set; }
        public string RoleName { get; init; } = string.Empty;
        public bool IsActive { get; init; } 
    }
}
