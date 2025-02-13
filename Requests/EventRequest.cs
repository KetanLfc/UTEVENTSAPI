using System.ComponentModel.DataAnnotations;
using UTEvents.Enums;

namespace UTEvents.Requests
{
    public class EventRequest
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public EventStatus Status { get; set; }

        [Required]
        public EventScope Scope { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

    }
}
