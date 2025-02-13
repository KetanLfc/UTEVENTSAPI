using System.ComponentModel.DataAnnotations;
using UTEvents.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    public Guid? GroupId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public virtual Role Role { get; set; } = null!;

    public virtual Group? Group { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
