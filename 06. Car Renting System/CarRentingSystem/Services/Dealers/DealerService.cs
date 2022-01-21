namespace CarRentingSystem.Services.Dealers
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;

    using Microsoft.EntityFrameworkCore;

    public class DealerService : IDealerService
    {
        private readonly CarRentingDbContext dbContext;

        public DealerService(CarRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> UserIsDealer(string userId)
            => await this.dbContext.Dealers.AnyAsync(d => d.UserId == userId);

        public async Task<int> GetDealerId(string userId)
            => await this.dbContext.Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefaultAsync();

        public async Task<int> CreateDealer(string name, string phoneNumber, string userId)
        {
            var dealer = new Dealer(name, phoneNumber, userId);

            await this.dbContext.AddAsync(dealer);
            await this.dbContext.SaveChangesAsync();

            return dealer.Id;
        }
    }
}
