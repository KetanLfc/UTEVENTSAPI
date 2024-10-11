using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTEvents.Enums;

namespace UTEvents.Entities
{
    [Table("AllowedEventRoles")]
    public class AllowedEventRole
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public EventRole EventRole { get; set; }

        [Required]
        public bool CanSubscribe { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
