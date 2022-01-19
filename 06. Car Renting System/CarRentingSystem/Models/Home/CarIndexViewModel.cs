namespace CarRentingSystem.Models.Home
{
    public class CarIndexViewModel
    {
        public CarIndexViewModel(int id, string brand, string model, string imageUrl, int year)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.ImageUrl = imageUrl;
            this.Year = year;
        }

        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
    }
}
