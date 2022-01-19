namespace CarRentingSystem.Models.Cars
{
    public class CarListingViewModel
    {
        public CarListingViewModel(int id, string brand, string model, string imageUrl, string category, int year)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.ImageUrl = imageUrl;
            this.Category = category;
            this.Year = year;
        }

        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }

        public int Year { get; init; }
    }
}
