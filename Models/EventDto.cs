using UTEvents.Enums;

namespace UTEvents.Models
{
    public record EventDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string CategoryName { get; init; } = string.Empty;
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }
        public string Description { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public EventStatus Status { get; init; }
        public EventScope Scope { get; init; }
        public Guid LocationId { get; init; }

        // Relationships
        public virtual LocationDto Location { get; init; } = null!;
        public virtual IEnumerable<AllowedEventRoleDto> AllowedEventRoles { get; init; } = new List<AllowedEventRoleDto>();
        public virtual IEnumerable<UserDto> Users { get; init; } = new List<UserDto>();
    }
}
