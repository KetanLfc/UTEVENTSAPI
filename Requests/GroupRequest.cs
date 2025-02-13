using System.ComponentModel.DataAnnotations;

namespace UTEvents.Requests
{
    public class GroupRequest
    {
        public Guid? Id { get; set; }

        [Required]
        public string GroupName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
