using Microsoft.AspNetCore.Authorization;

namespace WeatherForecastAPI.Authorization
{
    /// <summary>
    /// Handler for the HasScopeRequirement.
    /// </summary>
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        /// <summary>
        /// Checks if the user has the required scope.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to check for.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasScopeRequirement requirement
        )
        {
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            string[] scopes = context
                .User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)
                .Value.Split(' ');

            if (scopes.Any(scope => scope == requirement.Scope))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
