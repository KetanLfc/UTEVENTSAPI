using Microsoft.Identity.Web;

namespace UTEvents.Providers
{
    public class ClaimsProvider : IClaimsProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.GetObjectId() ?? string.Empty;

        public string UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value ?? string.Empty;

        public string UserFirstName => _httpContextAccessor.HttpContext?.User?.FindFirst("given_name")?.Value ?? string.Empty;

        public string UserLastName => _httpContextAccessor.HttpContext?.User?.FindFirst("family_name")?.Value ?? string.Empty;

        public string UserRole => _httpContextAccessor.HttpContext?.User?.FindFirst("role")?.Value ?? string.Empty;
    }
}
