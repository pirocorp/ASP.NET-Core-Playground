namespace CarRentingSystem.Services.Models.Cars
{
    public class CarServiceModel : ICarModel
    {
        public CarServiceModel(
            int id,
            string brand,
            string categoryName,
            string model,
            string imageUrl,
            bool isPublic,
            int year)
        {
            this.Id = id;
            this.Brand = brand;
            this.CategoryName = categoryName;
            this.Model = model;
            this.ImageUrl = imageUrl;
            this.IsPublic = isPublic;
            this.Year = year;
        }

        public int Id { get; init; }

        public string Brand { get; set; }

        public string CategoryName { get; init; }

        public string Model { get; set; }

        public string ImageUrl { get; init; }

        public bool IsPublic { get; set; }

        public int Year { get; init; }
    }
}
