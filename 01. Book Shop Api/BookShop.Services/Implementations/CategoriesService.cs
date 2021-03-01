namespace BookShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookShop.Common.Mapping;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Category;

    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly BookShopDbContext dbContext;

        public CategoriesService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(int id) => 
            await this.dbContext.Categories.AnyAsync(c => c.Id.Equals(id));

        public async Task<bool> ExistsAsync(string name)
            => await this.dbContext.Categories.AnyAsync(c => c.Name.ToLower().Equals(name.ToLower()));

        public async Task<IEnumerable<CategoryDetailsServiceModel>> AllAsync()
            => await this.dbContext.Categories
                .To<CategoryDetailsServiceModel>()
                .ToListAsync();

        public async Task<CategoryDetailsServiceModel> DetailsAsync(int id)
            => await this.dbContext.Categories
                .Where(c => c.Id.Equals(id))
                .To<CategoryDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category()
            {
                Name = name
            };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task<int> UpdateAsync(string name, int id)
        {
            var category = await this.dbContext.Categories.FirstAsync(c => c.Id.Equals(id));

            category.Name = name;
            await this.dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await this.dbContext.Categories
                .Where(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();

            this.dbContext.Remove(category);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
