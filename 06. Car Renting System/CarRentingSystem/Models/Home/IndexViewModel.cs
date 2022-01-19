namespace CarRentingSystem.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Cars = new List<CarIndexViewModel>();
        }

        public int TotalCars { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRents { get; init; }

        public IEnumerable<CarIndexViewModel> Cars { get; set; }
    }
}
