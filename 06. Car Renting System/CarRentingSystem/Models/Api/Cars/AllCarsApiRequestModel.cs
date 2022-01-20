namespace CarRentingSystem.Models.Api.Cars
{
    using CarRentingSystem.Models.Cars;

    public class AllCarsApiRequestModel
    {
        public AllCarsApiRequestModel()
        {
            this.Page = 1;
            this.CarsPerPage = 10;
        }

        public string? Brand { get; init; }

        public int CarsPerPage { get; init; }

        public int Page { get; init; }

        public string? SearchTerm { get; init; }

        public CarSorting Sorting { get; init; }
    }
}
