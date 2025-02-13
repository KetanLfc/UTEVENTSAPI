namespace UTEvents.Providers
{
    public interface IClaimsProvider
    {
        string UserId { get; }
        string UserEmail { get; }
        string UserFirstName { get; }
        string UserLastName { get; }
        string UserRole { get; }
    }
}
