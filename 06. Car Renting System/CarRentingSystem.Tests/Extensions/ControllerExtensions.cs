namespace CarRentingSystem.Tests.Extensions
{
    using System;
    using System.Linq;

    using CarRentingSystem.Tests.Mocks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static bool ControllerHasAttribute<TAttribute>(this ControllerBase controller)
            => controller
                .GetType()
                .GetCustomAttributes(typeof(TAttribute), true)
                .Any();

        public static bool MethodHasAttribute<TAttribute>(
            this ControllerBase controller,
            string methodName,
            Type[] parametersTypes)
                => controller
                    .GetType()
                    .GetMethod(methodName, parametersTypes)
                    ?.GetCustomAttributes(typeof(TAttribute), true)
                    .Any() ?? false;

        public static bool MethodIsForAuthorizedRequestsOnly(
            this ControllerBase controller,
            string methodName,
            Type[] parametersTypes)
            => (controller.ControllerHasAttribute<AuthorizeAttribute>()
               && !controller.MethodHasAttribute<AllowAnonymousAttribute>(methodName, parametersTypes))
               || controller.MethodHasAttribute<AuthorizeAttribute>(methodName, parametersTypes);

        public static T WithUserIdentifier<T>(this T? controller, string? userId = null)
            where T : ControllerBase
        {
            userId ??= Guid.NewGuid().ToString();

            if (controller?.ControllerContext.HttpContext is null)
            {
                controller!.ControllerContext.HttpContext = new DefaultHttpContext()
                {
                    User = ClaimsPrincipalMock.WithIdentifier(userId),
                };
            }
            else
            {
                controller.HttpContext.User = ClaimsPrincipalMock.WithIdentifier(userId);
            }

            return controller;
        }
    }
}
