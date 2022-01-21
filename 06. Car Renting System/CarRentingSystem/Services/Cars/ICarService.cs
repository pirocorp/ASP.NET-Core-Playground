namespace CarRentingSystem.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Home;
    using CarRentingSystem.Services.Models.Cars;

    public interface ICarService
    {
        Task<bool> CarIsOwnedByDealer(int carId, int dealerId);

        Task<CarDetailsServiceModel?> GetCarDetails(int carId);

        Task<IEnumerable<CarIndexViewModel>> GetLatestCars(int n);

        Task<CarQueryServiceModel> GetCars(
            string? brand,
            string? searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        Task<IEnumerable<CarServiceModel>> GetUserCars(string userId);

        Task<IEnumerable<string>> GetBrands();

        Task<IEnumerable<CarCategoryServiceModel>> GetCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<int> CreateCar(
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId,
            int dealerId);

        Task<bool> EditCar(
            int carId,
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId);
    }
}
