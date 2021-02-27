namespace BookShop.Api.Controllers
{
    using System;
    using BookShop.Api.Infrastructure.Extensions;
    using BookShop.Services;

    using Microsoft.AspNetCore.Mvc;
    using Models.Authors;

    using System.Threading.Tasks;
    using Infrastructure.Filters;
    using static WebConstants;

    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id) => this.OkOrNotFound(await this.authorService.DetailsAsync(id));

        [HttpGet(WithId + "/books")]
        public async Task<IActionResult> GetBooks(int id) => this.Ok(await this.authorService.Books(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] PostAuthorRequestModel model)
            => this.Ok(await this.authorService.CreateAsync(model.FirstName, model.LastName));
    }
}
