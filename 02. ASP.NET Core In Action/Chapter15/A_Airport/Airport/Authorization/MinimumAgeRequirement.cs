namespace Airport.Authorization
{
    using Microsoft.AspNetCore.Authorization;
    
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int minimumAge)
        {
            this.MinimumAge = minimumAge;
        }

        public int MinimumAge { get; }
    }
}