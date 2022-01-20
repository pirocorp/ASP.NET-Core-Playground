namespace CarRentingSystem.Services.Cars
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarRentingSystem.Models;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Models.Home;
    using Models.Cars;

    public interface ICarService
    {
        Task<IEnumerable<CarIndexViewModel>> GetLatest(int n);

        Task<CarQueryServiceModel> GetCars(
            string? brand,
            string? searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage);

        Task<IEnumerable<string>> GetBrands();

        Task Add(
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId,
            int dealerId);
    }
}
