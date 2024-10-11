using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTEvents.Enums;

namespace UTEvents.Entities
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public EventStatus Status { get; set; }

        [Required]
        public EventScope Scope { get; set; }

        public virtual ICollection<AllowedEventRole> AllowedEventRoles { get; set; } = new List<AllowedEventRole>();

        public virtual Location Location { get; set; } = null!;

        public virtual EventCategory EventCategory { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
