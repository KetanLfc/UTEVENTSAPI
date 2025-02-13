using UTEvents.Enums;

namespace UTEvents.Requests
{
    public class EventStatusRequest
    {
        public Guid Id { get; set; }
        public EventStatus Status { get; set; }
    }
}
