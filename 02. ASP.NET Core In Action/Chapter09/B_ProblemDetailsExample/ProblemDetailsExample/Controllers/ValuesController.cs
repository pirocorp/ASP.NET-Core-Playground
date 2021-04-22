namespace ProblemDetailsExample.Controllers
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet("{myValue?}")]
        public IActionResult Get([Required] string myValue)
        {
            // Never called, due to automatic invalid response generation
            return this.Ok(myValue);
        }
    }
}
