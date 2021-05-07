namespace ConfigureOptionsExample.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : Controller
    {
        private readonly CurrencyOptions _currencies;

        public CurrenciesController(IOptions<CurrencyOptions> currencies)
        {
            this._currencies = currencies.Value;
        }

        [HttpGet]
        public ActionResult<CurrencyOptions> Get()
        {
            return this._currencies;
        }

    }
}
