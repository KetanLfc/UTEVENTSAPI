using UTEvents.Enums;

namespace UTEvents.Requests
{
    public class AllowedEventRoleRequest
    {
        public Guid EventId { get; set; }
        public EventRole EventRole { get; set; }
        public bool CanSubscribe { get; set; }
    }
}
