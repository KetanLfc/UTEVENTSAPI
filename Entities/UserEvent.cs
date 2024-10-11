using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTEvents.Enums;

namespace UTEvents.Entities
{
    [Table("UserEvents")]
    public class UserEvent
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [Required]
        public RSVPStatus RSVPStatus { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual Event Event { get; set; } = null!;
    }
}
