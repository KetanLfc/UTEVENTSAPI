using System.ComponentModel.DataAnnotations;

public record UserRequest
{
    public Guid? Id { get; init; }

    [Required]
    public Guid RoleId { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Name { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty; 

    public bool IsActive { get; init; }
    public Guid? GroupId { get; init; }
}
