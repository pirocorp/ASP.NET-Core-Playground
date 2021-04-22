namespace UsingApiControllerAttribute.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class FruitController : ControllerBase
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
        public IActionResult Update(UpdateModel model)
        {
            if (model.Id < 0 || model.Id > this._fruit.Count)
            {
                return this.NotFound();
            }

            this._fruit[model.Id] = model.Name;
            return this.Ok();
        }
    }
}
