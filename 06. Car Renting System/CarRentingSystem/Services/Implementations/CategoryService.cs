namespace CarRentingSystem.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarRentingSystem.Data;
    using CarRentingSystem.Models.Cars;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly CarRentingDbContext dbContext;

        public CategoryService(CarRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CarCategoryViewModel>> GetCategories()
            => await this.dbContext.Categories.Select(c => new CarCategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();

        public async Task<bool> Exists(int id)
            => await this.dbContext.Categories.AnyAsync(c => c.Id == id);
    }
}
