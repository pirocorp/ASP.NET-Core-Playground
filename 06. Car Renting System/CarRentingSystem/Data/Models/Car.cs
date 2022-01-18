namespace CarRentingSystem.Data.Models
{
    using CarRentingSystem.Infrastructure.Exceptions;

    public class Car
    {
        private Category? category;

        public Car(
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId)
        {
            this.Brand = brand;
            this.Model = model;
            this.Description = description;
            this.ImageUrl = imageUrl;
            this.Year = year;
            this.CategoryId = categoryId;
        }

        public int Id { get; init; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public Category Category
        {
            get => this.category ?? throw new UninitializedPropertyException(nameof(this.Category));
            set => this.category = value;
        }
    }
}
