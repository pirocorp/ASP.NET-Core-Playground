namespace RecipeApplication.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using RecipeApplication.Models;

    [Route("api/nofilters")]
    public class NoFiltersApiController : ControllerBase
    {
        private const bool IsEnabled = true;
        private readonly IPAddress[] _allowedAddress =
        {
            IPAddress.Parse("127.0.0.1"),
            IPAddress.Parse("::1"),
        };

        private readonly RecipeService _service;

        public NoFiltersApiController(RecipeService service)
        {
            this._service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ipAddress = this.HttpContext
                .Connection.RemoteIpAddress;
            if (!this._allowedAddress.Contains(ipAddress))
            {
                return this.Forbid();
            }

            if (!IsEnabled) { return this.BadRequest(); }

            try
            {
                if (!await this._service.DoesRecipeExistAsync(id))
                {
                    return this.NotFound();
                }

                var detail = await this._service.GetRecipeDetail(id);

                var lastModified = this.Request.GetTypedHeaders().IfModifiedSince;
                if (lastModified.HasValue)
                {
                    if (lastModified >= detail.LastModified)
                    {
                        return this.StatusCode(304);
                    }
                }

                this.Response.GetTypedHeaders().LastModified =
                    detail.LastModified;

                return this.Ok(detail);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> EditAsync(
            int id, [FromBody] UpdateRecipeCommand command)
        {
            var ipAddress = this.HttpContext
                .Connection.RemoteIpAddress;
            if (!this._allowedAddress.Contains(ipAddress))
            {
                return this.Forbid();
            }

            if (!IsEnabled) { return this.BadRequest(); }

            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                if (!await this._service.DoesRecipeExistAsync(id))
                {
                    return this.NotFound();
                }

                await this._service.UpdateRecipe(command);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex);
            }
        }

        private static IActionResult GetErrorResponse(Exception ex)
        {
            var error = new ProblemDetails
            {
                Title = "An error occured",
                Detail = ex.Message,
                Status = 500,
                Type = "https://httpstatuses.com/500"
            };

            return new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}