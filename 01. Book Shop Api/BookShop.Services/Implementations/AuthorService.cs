namespace BookShop.Services.Implementations
{
    using System.Linq;
    using Common.Mapping;
    using Data;
    using Models.Author;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext dbContext;

        public AuthorService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AuthorDetailsServiceModel Details(int id)
            => this.dbContext.Authors
                .Where(a => a.Id.Equals(id))
                .To<AuthorDetailsServiceModel>().FirstOrDefault();
    }
}
