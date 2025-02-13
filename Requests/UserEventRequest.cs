using UTEvents.Enums;

namespace UTEvents.Requests
{
    public class UserEventRequest
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public RSVPStatus RSVPStatus { get; set; }
    }
}
