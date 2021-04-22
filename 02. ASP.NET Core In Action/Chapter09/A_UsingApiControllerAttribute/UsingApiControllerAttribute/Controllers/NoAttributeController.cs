namespace UsingApiControllerAttribute.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class NoAttributeController : ControllerBase
    {
        private readonly List<string> _fruit = new()
        {
            "Pear", "Lemon", "Peach"
        };

        [HttpGet]
        public IActionResult Update()
        {
            return this.Ok(this._fruit);
        }

        [HttpPost]
        public IActionResult Update([FromBody] UpdateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            if (model.Id < 0 || model.Id > this._fruit.Count)
            {
                return this.NotFound(new ProblemDetails()
                {
                    Status = 404,
                    Title = "Not Found",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                });
            }

            this._fruit[model.Id] = model.Name;

            return this.Ok();
        }
    }
}
