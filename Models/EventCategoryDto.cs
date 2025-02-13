using System.ComponentModel.DataAnnotations;

namespace UTEvents.Models
{
    public record EventCategoryDto
    {
        [Required]
        public string CategoryName { get; init; } = string.Empty;
    }
}
