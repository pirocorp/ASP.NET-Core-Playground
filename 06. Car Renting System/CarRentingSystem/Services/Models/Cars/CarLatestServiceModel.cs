namespace CarRentingSystem.Services.Models.Cars
{
    public class CarLatestServiceModel : ICarModel
    {
        public CarLatestServiceModel(int id, string brand, string model, string imageUrl, int year)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.ImageUrl = imageUrl;
            this.Year = year;
        }

        public int Id { get; init; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
    }
}
