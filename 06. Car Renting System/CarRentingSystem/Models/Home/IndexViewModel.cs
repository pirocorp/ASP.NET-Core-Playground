namespace CarRentingSystem.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Cars = new List<CarIndexViewModel>();
        }

        public IEnumerable<CarIndexViewModel> Cars { get; set; }
    }
}
