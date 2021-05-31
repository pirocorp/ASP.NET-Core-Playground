namespace Airport.Authorization
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    public class FrequentFlyerHandler : AuthorizationHandler<AllowedInLoungeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowedInLoungeRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(Claims.FrequentFlyerClass, "Gold");

            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}