namespace CarRentingSystem.Services.Cars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models;
    using CarRentingSystem.Services.Models.Cars;

    using Microsoft.EntityFrameworkCore;

    public class CarService : ICarService
    {
        private readonly CarRentingDbContext dbContext;
        private readonly IMapper mapper;

        public CarService(
            CarRentingDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> CarIsOwnedByDealer(int carId, int dealerId)
            => await this.dbContext.Cars.AnyAsync(c => c.Id == carId && c.DealerId == dealerId);

        public async Task<CarDetailsServiceModel?> GetCarDetails(int carId)
            => await this.dbContext.Cars
                .Where(c => c.Id == carId)
                .ProjectTo<CarDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<CarLatestServiceModel>> GetLatestCars(int n)
            => await this.dbContext.Cars
                .OrderByDescending(c => c.Id)
                .ProjectTo<CarLatestServiceModel>(this.mapper.ConfigurationProvider)
                .Take(n)
                .ToListAsync();

        public async Task<CarQueryServiceModel> GetCars(
            string? brand,
            string? searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
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

            carsQuery = carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage);

            var cars = await GetCars(carsQuery);

            var result = new CarQueryServiceModel()
            {
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                TotalCars = total,
                Cars = cars,
            };

            return result;
        }

        public async Task<IEnumerable<CarServiceModel>> GetUserCars(string userId)
            => await GetCars(this.dbContext.Cars
                .Where(c => c.Dealer.UserId == userId));

        public async Task<IEnumerable<string>> GetBrands()
            => await this.dbContext.Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

        public async Task<IEnumerable<CarCategoryServiceModel>> GetCategories()
            => await this.dbContext.Categories.Select(c => new CarCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

        public async Task<bool> CategoryExists(int categoryId)
            => await this.dbContext.Categories.AnyAsync(c => c.Id == categoryId);

        public async Task<int> CreateCar(
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

            return car.Id;
        }

        public async Task<bool> EditCar(
            int carId,
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId)
        {
            var carData = await this.dbContext.Cars.FindAsync(carId);

            if (carData is null)
            {
                return false;
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
            carData.Year = year;
            carData.CategoryId = categoryId;

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        private static async Task<IEnumerable<CarServiceModel>> GetCars(IQueryable<Car> carsQuery)
            => await carsQuery
                .Select(c => new CarServiceModel(
                    c.Id,
                    c.Brand,
                    c.Category.Name,
                    c.Model,
                    c.ImageUrl,
                    c.Year))
                .ToListAsync();
    }
}
