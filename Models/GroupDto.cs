namespace UTEvents.Models
{
    public record GroupDto
    {
        public Guid Id { get; init; }
        public string GroupName { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
