namespace BackgroundServiceCache.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ExchangeRatesCache _cache;

        public ValuesController(ExchangeRatesCache cache)
        {
            this._cache = cache;
        }

        [HttpGet]
        public ActionResult<ExchangeRates> TypedClient()
        {
            return this._cache.GetLatestRates();
        }
    }
}
