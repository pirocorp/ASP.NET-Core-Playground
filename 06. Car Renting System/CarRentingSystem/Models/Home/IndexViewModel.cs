namespace CarRentingSystem.Models.Home
{
    using System.Collections.Generic;

    using CarRentingSystem.Services.Models.Cars;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Cars = new List<CarLatestServiceModel>();
        }

        public IEnumerable<CarLatestServiceModel> Cars { get; set; }
    }
}
