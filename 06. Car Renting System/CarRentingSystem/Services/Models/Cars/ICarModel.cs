namespace CarRentingSystem.Services.Models.Cars
{
    public interface ICarModel
    {
        string Brand { get; set; }

        string Model { get; set; }

        int Year { get; init; }
    }
}
