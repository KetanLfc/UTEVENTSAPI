using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UTEvents.Entities
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string GroupName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
