using UTEvents.Enums;

namespace UTEvents.Models
{
    public record AllowedEventRoleDto
    {
        public Guid EventId { get; init; }
        public EventRole EventRole { get; init; }
        public bool CanSubscribe { get; init; }
    }
}
