namespace CarRentingSystem.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

        public static bool IsAdmin(this ClaimsPrincipal principal)
            => principal.IsInRole(AdministratorRoleName);
    }
}
