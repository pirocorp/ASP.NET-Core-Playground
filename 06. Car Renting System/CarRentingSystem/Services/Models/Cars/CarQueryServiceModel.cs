namespace CarRentingSystem.Services.Models.Cars
{
    using System.Collections.Generic;

    public class CarQueryServiceModel
    {
        public CarQueryServiceModel()
        {
            this.Cars = new List<CarServiceModel>();
        }

        public int CarsPerPage { get; init; }

        public int CurrentPage { get; set; }

        public int TotalCars { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; }
    }
}
