namespace BookShop.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BookShop.Common.Mapping;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Author;

    using Microsoft.EntityFrameworkCore;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext dbContext;

        public AuthorService(BookShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAsync(string firstName, string lastName)
        {
            var author = new Author()
            {
                FirstName = firstName,
                LastName = lastName
            };

            await this.dbContext.AddAsync(author);
            await this.dbContext.SaveChangesAsync();

            return author.Id;
        }

        public async Task<AuthorDetailsServiceModel> DetailsAsync(int id)
            => await this.dbContext.Authors
                .Where(a => a.Id.Equals(id))
                .To<AuthorDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<BooksByAuthorServiceModel>> Books(int authorId)
            => await this.dbContext.Books
                .Where(b => b.AuthorId.Equals(authorId))
                .To<BooksByAuthorServiceModel>()
                .ToListAsync();
    }
}
