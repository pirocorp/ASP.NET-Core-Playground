namespace BookShop.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound(this Controller controller, object model)
        {
            if (model is null)
            {
                return controller.NotFound();
            }

            return controller.Ok(model);
        }
    }
}
