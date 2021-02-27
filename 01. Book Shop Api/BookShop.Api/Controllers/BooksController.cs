namespace BookShop.Api.Controllers
{
    using System.Threading.Tasks;

    using BookShop.Api.Infrastructure.Extensions;
    using BookShop.Api.Infrastructure.Filters;
    using BookShop.Api.Models.Books;
    using BookShop.Services;

    using Microsoft.AspNetCore.Mvc;
    using static WebConstants;

    public class BooksController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search = "")
            => this.Ok(await this.bookService.AllAsync(search));

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id) => this.OkOrNotFound(await this.bookService.DetailsAsync(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CreateBookRequestModel model)
        {
            if (!await this.authorService.ExistsAsync(model.AuthorId))
            {
                return this.BadRequest("Author does not exist.");
            }

            var id = await this.bookService.Create(
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return this.Ok(id);
        }
    }
}
