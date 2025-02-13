using UTEvents.Enums;

namespace UTEvents.Models
{
    public record UserEventDto
    {
        public Guid UserId { get; init; }
        public Guid EventId { get; init; }
        public string EventRole { get; init; } = string.Empty;
        public RSVPStatus RSVPStatus
        {
            get; init;
        }
    }
}
