using Microsoft.AspNetCore.Authorization;

namespace WeatherForecastAPI.Authorization
{
    public class HasScopeRequirement: IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasScopeRequirement(string issuer, string scope)
        {
            Issuer = issuer ?? throw new ArgumentException(nameof(issuer));
            Scope = scope ?? throw new ArgumentException(nameof(scope));
        }
    }
}
