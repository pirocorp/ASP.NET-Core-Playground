namespace CarRentingSystem.Services.Models.Cars
{
    public class CarServiceModel
    {
        public CarServiceModel(int id, string brand, string categoryName, string model, string imageUrl, int year)
        {
            this.Id = id;
            this.Brand = brand;
            this.CategoryName = categoryName;
            this.Model = model;
            this.ImageUrl = imageUrl;
            this.Year = year;
        }

        public int Id { get; init; }

        public string Brand { get; init; }

        public string CategoryName { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
    }
}
