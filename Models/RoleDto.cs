namespace UTEvents.Models
{
    public record RoleDto
    {
        public Guid Id { get; init; }
        public string RoleName { get; init; } = string.Empty;
    }
}
