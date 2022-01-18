namespace CarRentingSystem.Services.Implementations
{
    using System.Threading.Tasks;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;

    public class CarService : ICarService
    {
        private readonly CarRentingDbContext dbContext;

        public CarService(CarRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCar(
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId)
        {
            var car = new Car(brand, model, description, imageUrl, year, categoryId);

            await this.dbContext.AddAsync(car);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
