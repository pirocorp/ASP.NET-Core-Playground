namespace BackgroundServiceDatabaseCache.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BackgroundServiceDatabaseCache.Data;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ValuesController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ExchangeRates>> TypedClientAsync()
        {
            return await this._context.ExchangeRates
                .Include(rates => rates.Rates)
                .OrderByDescending(rates => rates.ExchangeRatesId)
                .FirstOrDefaultAsync();
        }
    }
}
