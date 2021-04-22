using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CarsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        /// <summary>
        /// This represents the global application model that would
        /// normally be stored in a database etc
        /// </summary>
        private static readonly List<Car> Cars = new() { };

        private const string _carsAsXml = "<cars><car>Nissan Micra</car><car>FordFocus</car></cars>";

        [HttpGet("start")]
        [HttpGet("ignition")]
        [HttpGet("/start-car")]
        public IEnumerable<string> ListCars()
        {
            return new string[]
                { "Nissan Micra", "Ford Focus" };
        }

        [HttpGet("null")]
        public IActionResult Null()
        {
            return this.Ok(null);
        }

        [HttpGet("content")]
        public string ListCarsAsContent()
        {
            return _carsAsXml;
        }

        [HttpGet("xml")]
        public IActionResult ListCarsAsXml()
        {
            return this.Content(_carsAsXml, "text/xml");
        }

        [HttpGet("json")]
        public IActionResult ListCarsAsJson()
        {
            return new JsonResult(new string[]
                { "Nissan Micra", "FordFocus" });
        }

        [HttpPost]
        public IActionResult Add(Car car)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Cars.Add(car);
            return this.Ok();
        }
    }
}