namespace CarRentingSystem.Services
{
    using System.Threading.Tasks;

    using CarRentingSystem.Data.Models;

    public interface IDealerService
    {
        Task<bool> UserIsDealer(string userId);

        Task<int> GetDealerId(string userId);

        Task Add(string name, string phoneNumber, string userId);
    }
}
