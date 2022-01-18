namespace CarRentingSystem.Services
{
    using System.Threading.Tasks;

    public interface ICarService
    {
        Task AddCar(string brand, string model, string description, string imageUrl, int year, int categoryId);
    }
}
