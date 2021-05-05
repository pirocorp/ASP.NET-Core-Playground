namespace Airport.Authorization
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    public class IsAirlineEmployeeHandler : AuthorizationHandler<AllowedInLoungeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowedInLoungeRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == Claims.EmployeeNumber))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}