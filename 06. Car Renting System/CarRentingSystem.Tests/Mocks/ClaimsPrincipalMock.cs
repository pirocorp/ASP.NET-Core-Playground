namespace CarRentingSystem.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    public static class ClaimsPrincipalMock
    {
        public static ClaimsPrincipal WithIdentifier(string userId)
            => Build(new Claim[]
            {
                new (ClaimTypes.NameIdentifier, userId),
            });

        private static ClaimsPrincipal Build(IEnumerable<Claim> claims)
            => new (new ClaimsIdentity(claims, Guid.NewGuid().ToString()));
    }
}
