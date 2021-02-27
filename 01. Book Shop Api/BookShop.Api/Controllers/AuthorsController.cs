namespace BookShop.Api.Controllers
{
    using BookShop.Api.Infrastructure.Extensions;
    using BookShop.Services;

    using Microsoft.AspNetCore.Mvc;
    using Models.Authors;

    using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostAuthorRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.authorService.CreateAsync(model.FirstName, model.LastName);
            return this.Ok(result);
        }
    }
}
