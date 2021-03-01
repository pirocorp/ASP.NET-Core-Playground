namespace BookShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BookShop.Services.Models.Category;

    public interface ICategoriesService
    {
        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string name);

        Task<IEnumerable<CategoryDetailsServiceModel>> AllAsync();

        Task<CategoryDetailsServiceModel> DetailsAsync(int id);

        Task<int> CreateAsync(string name);

        Task<int> UpdateAsync(string name, int id);

        Task DeleteAsync(int id);
    }
}
