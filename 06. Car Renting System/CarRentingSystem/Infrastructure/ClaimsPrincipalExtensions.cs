﻿namespace CarRentingSystem.Infrastructure
{
    using System.Security.Claims;

    using static WebConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        public static bool IsAdmin(this ClaimsPrincipal principal)
            => principal.IsInRole(AdministratorRoleName);
    }
}
