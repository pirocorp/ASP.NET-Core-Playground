namespace CarRentingSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarRentingSystem.Models.Cars;

    public interface ICategoryService
    {
        Task<IEnumerable<CarCategoryViewModel>> GetCategories();

        Task<bool> Exists(int id);
    }
}
