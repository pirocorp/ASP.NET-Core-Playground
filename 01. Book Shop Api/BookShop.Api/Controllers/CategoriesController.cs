namespace BookShop.Api.Controllers
{
    using System.Threading.Tasks;

    using BookShop.Api.Infrastructure.Extensions;
    using BookShop.Api.Models.Categories;
    using BookShop.Services;

    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.categoriesService.AllAsync());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.categoriesService.DetailsAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryRequestModel model)
        {
            if (await this.categoriesService.ExistsAsync(model.Name))
            {
                return this.BadRequest("Category with this name already exists");
            }

            var id = await this.categoriesService.CreateAsync(model.Name);

            return this.Ok(id);
        }

        [HttpPut(WithId)]
        public async Task<IActionResult> Put([FromBody] CategoryRequestModel model, int id)
        {
            if (!await this.categoriesService.ExistsAsync(id))
            {
                return this.BadRequest("Category doesn't exists.");
            }

            id = await this.categoriesService.UpdateAsync(model.Name, id);

            return this.Ok(id);
        }

        [HttpDelete(WithId)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.categoriesService.ExistsAsync(id))
            {
                return this.BadRequest("Category doesn't exists.");
            }

            await this.categoriesService.DeleteAsync(id);
            return this.Ok();
        }
    }
}
