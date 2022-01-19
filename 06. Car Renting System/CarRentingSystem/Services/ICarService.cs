namespace CarRentingSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Models.Home;

    public interface ICarService
    {
        Task<IEnumerable<CarIndexViewModel>> GetLatest(int n);

        Task<(int Total, IEnumerable<CarListingViewModel> Cars)> GetAll(
            string? brand,
            string? searchTerm,
            CarSorting sorting,
            int currentPage);

        Task<IEnumerable<string>> GetBrands();

        Task<int> TotalCars();

        Task Add(string brand, string model, string description, string imageUrl, int year, int categoryId);
    }
}
