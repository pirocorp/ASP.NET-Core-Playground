namespace BookShop.Api.Controllers
{
    using BookShop.Services;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet(WithId)]
        public IActionResult Get(int id) => this.OkOrNotFound(this.authorService.Details(id));
    }
}
