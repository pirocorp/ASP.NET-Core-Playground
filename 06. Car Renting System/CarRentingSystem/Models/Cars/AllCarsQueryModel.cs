namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllCarsQueryModel
    {
        public AllCarsQueryModel()
        {
            this.Brands = new List<string>();
            this.Cars = new List<CarListingViewModel>();
            this.CurrentPage = 1;
        }

        public string? Brand { get; init; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<CarListingViewModel> Cars { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }

        public int TotalCars { get; set; }
    }
}
