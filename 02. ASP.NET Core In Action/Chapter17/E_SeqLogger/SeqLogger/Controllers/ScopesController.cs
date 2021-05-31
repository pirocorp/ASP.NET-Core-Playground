namespace SeqLogger.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class ScopesController : Controller
    {
        private readonly ILogger<ScopesController> _logger;

        public ScopesController(ILogger<ScopesController> logger)
        {
            this._logger = logger;
        }

        // GET api/scopes
        [HttpGet]
        public IActionResult Get()
        {
            this._logger.LogInformation("No, I don’t have scope");

            using (this._logger.BeginScope("Scope value"))
            using (this._logger.BeginScope(new Dictionary<string, object> { { "CustomValue1", 12345 } }))
            {
                this._logger.LogInformation("Yes, I have the scope!");
            }

            this._logger.LogInformation("No, I lost it again");

            return this.Ok();
        }
    }
}
