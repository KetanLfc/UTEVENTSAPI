namespace UTEvents.Models
{
    public record LocationDto
    {
        public Guid LocationId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
    }
}
