﻿namespace api.shopping.com.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    //[EnableCors] // Applies the default policy, specified in UseCors("AllowShoppingApp");
    //[EnableCors("AllowShoppingApp")] // Specify the policy to apply
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // [DisableCors] // Override the default policy or [EnableCors] attributes.
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {
               "Aunt Hattie's",
                "Danish",
                "Cobblestone",
                "Dave's Killer Bread",
                "Entenmann's",
                "Famous Amos",
                "Home Pride",
                "Hovis",
                "Keebler Company",
                "Kits",
                "McVitie's",
                "Mother's Pride",
            };
        }
    }
}
