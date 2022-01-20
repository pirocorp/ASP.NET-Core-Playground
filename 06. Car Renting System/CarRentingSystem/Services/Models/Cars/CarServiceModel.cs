namespace CarRentingSystem.Services.Models.Cars
{
    public class CarServiceModel
    {
        public CarServiceModel(int id, string brand, string category, string model, string imageUrl, int year)
        {
            this.Id = id;
            this.Brand = brand;
            this.Category = category;
            this.Model = model;
            this.ImageUrl = imageUrl;
            this.Year = year;
        }

        public int Id { get; init; }

        public string Brand { get; init; }

        public string Category { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
    }
}
