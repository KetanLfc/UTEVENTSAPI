using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UTEvents.Entities
{
    [Table("EventCategories")]
    public class EventCategory
    {
        [Key]
        public string CategoryName { get; set; } = string.Empty;

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
