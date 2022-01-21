namespace CarRentingSystem.Services.Dealers
{
    using System.Threading.Tasks;

    public interface IDealerService
    {
        Task<bool> UserIsDealer(string userId);

        Task<int> GetDealerId(string userId);

        Task<int> CreateDealer(string name, string phoneNumber, string userId);
    }
}
