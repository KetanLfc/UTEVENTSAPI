using System.ComponentModel.DataAnnotations;

namespace UTEvents.Requests
{
    public class LocationRequest
    {
        public Guid? LocationId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;
    }
}
