namespace CarRentingSystem.Services.Models.Cars
{
    public class CarDetailsServiceModel : CarServiceModel
    {
        public CarDetailsServiceModel(
            int id,
            string brand,
            int categoryId,
            string categoryName,
            string model,
            string imageUrl,
            int year,
            int dealerId,
            string dealerName,
            string description,
            string userId)
            : base(id, brand, categoryName, model, imageUrl, year)
        {
            this.CategoryId = categoryId;
            this.DealerId = dealerId;
            this.DealerName = dealerName;
            this.Description = description;
            this.UserId = userId;
        }

        public int CategoryId { get; set; }

        public int DealerId { get; init; }

        public string DealerName { get; init; }

        public string Description { get; init; }

        public string UserId { get; init; }
    }
}
