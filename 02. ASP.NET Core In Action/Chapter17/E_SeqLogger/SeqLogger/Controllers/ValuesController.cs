namespace SeqLogger.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly ValuesService _service;

        public ValuesController(ILogger<ValuesController> logger, ValuesService service)
        {
            this._logger = logger;
            this._service = service;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            this._logger.LogInformation("Inside controller, outside scope");

            using (this._logger.BeginScope("Some scope value"))
            using (this._logger.BeginScope(123))
            using (this._logger.BeginScope(new Dictionary<string, object> { { "ScopeValue1", "outer scope" } }))
            {
                this._logger.LogInformation("Inside controller, inside scope");
                return this._service.GetValues();
            }
        }
    }
}
