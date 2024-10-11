using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UTEvents.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

}
