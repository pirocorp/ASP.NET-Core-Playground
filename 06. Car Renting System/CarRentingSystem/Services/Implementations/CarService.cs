namespace CarRentingSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Infrastructure;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Models.Home;

    using Microsoft.EntityFrameworkCore;

    public class CarService : ICarService
    {
        private readonly CarRentingDbContext dbContext;

        public CarService(CarRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CarIndexViewModel>> GetLatest(int n)
            => await this.dbContext.Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel(c.Id, c.Brand, c.Model, c.ImageUrl, c.Year))
                .Take(n)
                .ToListAsync();

        public async Task<(int Total, IEnumerable<CarListingViewModel> Cars)> GetAll(
            string? brand,
            string? searchTerm,
            CarSorting sorting,
            int currentPage)
        {
            var carsQuery = this.dbContext.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery
                    .Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var tokens = searchTerm
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                searchTerm = string.Join(" ", tokens);

                carsQuery = carsQuery
                    .Where(c =>
                        (c.Brand.ToLower() + " " + c.Model.ToLower()).Contains(searchTerm.ToLower())
                        || c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var total = await carsQuery.CountAsync();
            var cars = await carsQuery
                .Skip((currentPage - 1) * UIConstants.CarsPerPage)
                .Take(UIConstants.CarsPerPage)
                .Select(c => new CarListingViewModel(c.Id, c.Brand, c.Model, c.ImageUrl, c.Category.Name, c.Year))
                .ToListAsync();

            return (total, cars);
        }

        public async Task<IEnumerable<string>> GetBrands()
            => await this.dbContext.Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

        public async Task<int> TotalCars() => await this.dbContext.Cars.CountAsync();

        public async Task Add(
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId,
            int dealerId)
        {
            var car = new Car(brand, model, description, imageUrl, year, categoryId, dealerId);

            await this.dbContext.AddAsync(car);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
